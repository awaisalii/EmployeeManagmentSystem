using BuisnessLayer.DTO.Request;
using BuisnessLayer.DTO.Response;
using BuisnessLayer.Entities;
using BuisnessLayer.HubConfig;
using DataAccessLayer.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
namespace EmployeeManagmentSystem.Controllers
{

    [Route("[controller]")]
    [ApiController]
    public class LoginController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration configuration;
        private readonly RoleManager<RoleModel> _roleManager;
        private readonly IActivities _activities;
        private readonly IHubContext<MyHub> _hubContext;

        public LoginController( UserManager<ApplicationUser> userManager , IHubContext<MyHub> hubContext, IConfiguration configuration, RoleManager<RoleModel> roleManager , IActivities activities )
        {
            this._userManager = userManager;
            this.configuration = configuration;
            this._roleManager = roleManager;
            this._activities = activities;
            this._hubContext = hubContext;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserResponseModel))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            if (loginRequest.Email != null && loginRequest.Password != null)
            {
                var user = await _userManager.FindByEmailAsync(loginRequest.Email);
                if (user != null && await _userManager.CheckPasswordAsync(user, loginRequest.Password))
                {
                   var roles = await _userManager.GetRolesAsync(user);
                    var expiration = DateTimeOffset.UtcNow.AddDays(30).ToUnixTimeSeconds();
                    var issuedAt = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
                    var claims = new List<Claim>
                    {
                        new Claim(JwtRegisteredClaimNames.Sub, configuration["Jwt:Subject"]),
                        new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                        new Claim("Id", user.Id.ToString()),
                        new Claim("UserName", user.UserName),
                        new Claim("Email", user.Email),
                        new Claim(ClaimTypes.NameIdentifier, user.Id), 
                        new Claim(ClaimTypes.Name, user.UserName),
                        new Claim(JwtRegisteredClaimNames.Iat, issuedAt.ToString(), ClaimValueTypes.Integer64), 
                        new Claim(JwtRegisteredClaimNames.Exp, expiration.ToString(), ClaimValueTypes.Integer64)
                    };
                    foreach (var role in roles)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, role));
                    }
                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken(
                        configuration["Jwt:Issuer"],
                        configuration["Jwt:Audience"],
                        claims,
                        expires: DateTime.UtcNow.AddDays(30),
                        signingCredentials: signIn
                    );
                    var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);
                    var imagePath = user.ImagePath != null ? $"{Request.Scheme}://{Request.Host}/uploads/images/{Uri.EscapeDataString(user.ImagePath)}" : null;
                    var userResponse = new UserResponseModel
                    {
                        Id = user?.Id,
                        UserName = user?.FirstName +" " + user.LastName,
                        Email = user?.Email,
                        Role = roles?[0],
                        ImagePath=imagePath,
                        Token = jwtToken
                    };
                    var activity = new Activities
                    {
                        Text = user?.FirstName + " " + user.LastName + " " + "logged In",
                        UserId = user.Id,
                        Date = DateTime.UtcNow,
                    };
                    await _activities.CreateActivity(activity);
                    return Ok(userResponse);
                }
                else
                {
                    var userWithEmail = await _userManager.FindByEmailAsync(loginRequest.Email);
                    if (userWithEmail != null)
                    {
                    var activity = new Activities
                    {
                        Text = user?.FirstName + " " + user.LastName + " " + "Failed Login attempt on " ,
                        UserId = user.Id,
                        Date = DateTime.UtcNow,
                    };
                    await _activities.CreateActivity(activity);
                    }
                    return Unauthorized("Invalid credentials.");
                }
            }
            else
            {
                await _hubContext.Clients.All.SendAsync("Notification", $" Failed Login attempt");
                return BadRequest("Email and Password are required.");
            }
        }

    }
}

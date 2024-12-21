using BuisnessLayer.DTO.Request;
using BuisnessLayer.Entities;
using DataAccessLayer.Repositories;
using Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace EmployeeManagmentSystem.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<RoleModel> _roleManager;
        private readonly IUser _iuser;
        private IConfiguration _configuration;
        private readonly ItokenService _itokenService;
        IWebHostEnvironment _env;
        public UserController(UserManager<ApplicationUser> userManager, RoleManager<RoleModel> roleManager , IUser Iuser , IConfiguration configuration, ItokenService _ItokenService , IWebHostEnvironment env )
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _roleManager = roleManager ?? throw new ArgumentNullException(nameof(roleManager));
            _iuser = Iuser;
            _configuration = configuration;
            _itokenService = _ItokenService;
            this._env = env;
        }




        



        [HttpPost("api/register")]
        public async Task<IActionResult> Register([FromForm] Register register)
        {
            if (register == null)
            {
                return BadRequest("Invalid registration details.");
            }
            string FileName = "";
            string imageUrl = "";
            if (register.Image !=null)
            {
                string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "Uploads", "images");
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }
                FileName = Guid.NewGuid().ToString() + "_"+ register.Image.FileName ;
                string filePath = Path.Combine(folderPath,FileName);
                register.Image.CopyTo(new FileStream(filePath,FileMode.Create));
                string requestScheme = HttpContext.Request.Scheme;
                string requestHost = HttpContext.Request.Host.Value;
                string baseUrl = _env.IsDevelopment()
                    ? $"{requestScheme}://{requestHost}/" 
                    : $"{requestScheme}://{requestHost}/";  
                imageUrl = $"{baseUrl}uploads/images/{FileName}";
            }
            var assignedToUser = _userManager.Users.Where(x=>x.Id==register.AssignedTo).FirstOrDefault();
            var user = new ApplicationUser
            {
                Email = register.Email,
                Position = register.Position,
                Salaried = register.Salaried,
                Department = register.Department,
                UserName = "@"+register.FirstName.ToLower(),
                FirstName = register.FirstName,
                LastName = register.LastName,
                HiredDate = register.HiredDate,
                BirthDate = register.BirthDate,
                Status = "1",
                Country = register.Country,
                Address = register.Address,
                State = register.State,
                City = register.City,
                PhoneNumber = register.PhoneNumber,
                ImagePath = imageUrl,
                AssignedTo = assignedToUser
            };

            var result = await _userManager.CreateAsync(user, register.PasswordHash);

            if (result.Succeeded)
            {
                var roleResult = await _userManager.AddToRoleAsync(user, "HR");

                if (roleResult.Succeeded)
                {
                    return Ok("User registered and role assigned successfully.");
                }
                else
                {
                    await _userManager.DeleteAsync(user);
                    return BadRequest($"User registration succeeded but role assignment failed: {string.Join(", ", roleResult.Errors.Select(e => e.Description))}");
                }
            }

            return BadRequest(result.Errors);
        }



        [HttpPost("api/CreateRole")]
        public async Task<IActionResult> CreateRole([FromBody] RoleModel role)
        {
            if (role == null || string.IsNullOrWhiteSpace(role.Name))
            {
                return BadRequest("Invalid role details.");
            }
            var roleExists = await _roleManager.RoleExistsAsync(role.Name);
            if (roleExists)
            {
                return Conflict("Role already exists.");
            }

            var result = await _roleManager.CreateAsync(role);

            if (result.Succeeded)
            {
                return Ok("Role created successfully.");
            }

            return BadRequest($"Error creating role: {string.Join(", ", result.Errors.Select(e => e.Description))}");
        }


        [HttpDelete("RemoveUser")]
        public async Task<IActionResult> Delete(string id)
        {
            var result=await _iuser.DeleteUser(id);
            return Ok(result);
        }

        [HttpGet("Single/{id}")]
        public async Task<IActionResult> Get(string id) 
        {
            var authHeader = Request.Headers["Authorization"].ToString();

            if (authHeader.StartsWith("Bearer "))
            {
                var token = authHeader.Substring("Bearer ".Length).Trim();

                var handler = new JwtSecurityTokenHandler();
                try
                {
                    var jwtToken = handler.ReadJwtToken(token);
                    var claims = jwtToken.Claims;
                    foreach (var claim in claims)
                    {
                        Console.WriteLine($"{claim.Type}: {claim.Value}");
                    }

                    var data= new { Claims = claims.Select(c => new { c.Type, c.Value }) };
                    var result = await _iuser.GetUser(id, Request.Scheme , Request.Host.ToString() );
                    return Ok(result);
                }
                catch (Exception ex)
                {
                    return BadRequest($"Invalid token: {ex.Message}");
                }
            }
            else
            {
                return Unauthorized("Token is missing or not prefixed with Bearer.");
            }
            
        }
        [HttpGet("Profile")]
        public async Task<IActionResult> Get()
{
            var authHeader = Request.Headers["Authorization"].ToString();
            var userdata = _itokenService.GetToken(authHeader);
            var result = await _iuser.GetUser(userdata.Id, Request.Scheme, Request.Host.ToString());
            return Ok(result);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateUser( [FromForm] UpdateUserRequest User)
        {
            string requestScheme = HttpContext.Request.Scheme;
            string requestHost = HttpContext.Request.Host.Value;
            var resuult = await _iuser.UpdateUserAsync(User, requestScheme, requestHost);
            return Ok(resuult);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result=await _iuser.GetAllUsers();
            return Ok(result);
        }

        [HttpDelete("DeleteRole")]
        public async Task<IActionResult> DeleteRole(string id)
        {
            var result= await _iuser.DeleteRole(id);
            return Ok(result);
        }


    }
}

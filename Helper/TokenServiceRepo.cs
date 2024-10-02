using DataAccessLayer.Repositories;
using Helper;
using System.IdentityModel.Tokens.Jwt;

public class TokenService : ItokenService
{
    public JwtTokenData GetToken(string authHeader)
    {
        if (authHeader.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
        {
            var token = authHeader.Substring("Bearer ".Length).Trim();
            var handler = new JwtSecurityTokenHandler();
            try
            {
                var jwtToken = handler.ReadJwtToken(token);
                var claims = jwtToken.Claims.ToList();

                var tokenData = new JwtTokenData
                {
                    Id = claims.FirstOrDefault(c => c.Type == "Id")?.Value,
                    UserName = claims.FirstOrDefault(c => c.Type == "UserName")?.Value,
                    Email = claims.FirstOrDefault(c => c.Type == "Email")?.Value,
                };

                return tokenData;
            }
            catch (Exception ex)
            {
                return null; 
            }
        }
        else
        {
            return null; 
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using VideoGameCharacterApi.Dtos;

namespace VideoGameCharacterApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IConfiguration configuration) : ControllerBase
    {
        [AllowAnonymous]
        [HttpPost("loggin")]
        public ActionResult<LoginResponse> Login(LoginRequest request)
        {
            if(request.Username == "user" && request.Password == "user123")
            {
                return Ok(new LoginResponse
                {
                    Token = GenerateToken("user", "User"),
                    Role = "User"
                });
            }
            if(request.Username=="admin" &&  request.Password == "admin123")
            {
                return Ok(new LoginResponse
                {
                    Token=GenerateToken("admin", "Admin"),
                    Role="Admin"
                });
            }
            return Unauthorized();
        }

        private string GenerateToken(string username, string role)
        {
            var key = configuration["Jwt:Key"]!;
            var issuer = configuration["Jwt:Issuer"]!;
            var audience = configuration["Jwt:Audience"]!;

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Role, role)
            };
            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials= new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}

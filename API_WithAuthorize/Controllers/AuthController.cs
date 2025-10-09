using API_WithAuthorize.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
namespace API_WithAuthorize.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _config;

        public AuthController(IConfiguration  configuration)
        {
            _config = configuration;
        }
        [AllowAnonymous]
        [HttpPost("Login")]
        public IActionResult Login([FromForm] Models.LoginRequest request)
        {
           
            if (request.UserName == "admin" && request.Password == "password")
            {
                string token = GenerateToken(request.UserName, "Admin");
                return Ok(new { Token = token });
            }

            return Unauthorized(new[] { new { Status=HttpStatusCode.Unauthorized,Message="Unauthorized" }});
        }


        private string GenerateToken(string UserName,string Role)
        {
            var securitykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Key"]));
            var credentials = new SigningCredentials(securitykey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier,UserName),
                new Claim(ClaimTypes.Role,Role)
            };

            var token = new JwtSecurityToken(_config["JWT:Issuer"],
                _config["JWT:Audience"],
                claims,
                expires: DateTime.Now.AddSeconds(30),
                signingCredentials: credentials
                 );
            return new JwtSecurityTokenHandler().WriteToken(token);

        }

    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using dataApi.Models;
using BCrypt.Net;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using JWT.Algorithms;
using System.IdentityModel.Tokens.Jwt;
using dataApi.Repositories;
using Microsoft.Extensions.Configuration;
using System.Text;

namespace dataApi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class SigninController : ControllerBase
    {
        private readonly IConfiguration _configuration; 
        private readonly Databasecontext _databaseContext;
        private readonly ILogger<SigninController> _logger;
        public SigninController(Databasecontext databasecontext, ILogger<SigninController> logger, IConfiguration config) {
        
            _databaseContext = databasecontext;
            _logger = logger;
            _configuration = config;
        }
        private string GenerateToken(User user)
        {
            List<Claim> claims = new List<Claim>()
            {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.Email, user.UserEmail),
                    new Claim("UserId", user.Id.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _configuration.GetValue<string>("auth:user")!
                )
                ) ;
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
            
                        var token = new JwtSecurityToken(
                            claims: claims,
                            expires: DateTime.Now.AddDays(2).AddHours(3),
                            signingCredentials: credentials
                            ) ;
            var jwt = new JwtSecurityTokenHandler().WriteToken(token) ;

            return jwt;
        }
        [HttpPost]
        public async Task<IActionResult> index([FromBody] SigninDto signin )
        {
           
            signin.UserEmail = signin.UserEmail?.Trim()!;
            signin.Password = signin.Password?.Trim()!;
    ;

            User user = new User();
            user.UserEmail = signin.UserEmail;
            user.Password = signin.Password;

            try
            {
                User userData = (User)await UserRepositories.FindUserByEmail(_databaseContext, user);
                if (userData == null)
                {
                    return NotFound("This user does not exist");
                }
                var IsPassword = BCrypt.Net.BCrypt.Verify(signin.Password, userData.Password);
                if (!IsPassword)
                {
                    return BadRequest("This is a wrong Password");
                }

                _logger.LogInformation(IsPassword.ToString());

               string tokenStr = GenerateToken(userData);

            return Ok(new List<object> { "Signin Successful", userData,tokenStr
            });
            }catch(Exception ex )
            {
                if (ex.InnerException != null)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError);
                }
                _logger.LogError(ex.Message.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }
    }
}

/*"userEmail": "mimi@gmail.com",
  "password": "string1234"*/
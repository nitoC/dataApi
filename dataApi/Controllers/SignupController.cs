using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using dataApi.Models;
using BCrypt.Net;
using dataApi.Repositories;

namespace dataApi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class SignupController : ControllerBase
    {
        private readonly Databasecontext _databasecontext;
        private readonly ILogger<SignupController> _logger;

        public SignupController(Databasecontext databasecontext, ILogger<SignupController> logger)
        {

            _databasecontext = databasecontext;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Index([FromBody] UserDto signup)
        {
            signup.Name = signup.Name?.Trim();
            signup.UserEmail = signup.UserEmail?.Trim();
            signup.Mobile = signup.Mobile?.Trim();
            signup.Password = signup.Password?.Trim();
            signup.UserName = signup.UserName?.Trim();

            var Hash = BCrypt.Net.BCrypt.HashPassword(signup.Password);

            User user = new User();
            user.Name = signup.Name;
            user.UserName = signup.UserName;
            user.UserEmail = signup.UserEmail;
            user.Age = signup.Age;
            user.Mobile = signup.Mobile;
            user.Password = Hash;

            try
            {
                var userData = (User)await UserRepositories.FindUserByEmail(_databasecontext, user);
                if(userData != null)
                {
                    return BadRequest( "user Exists Login");
                }
                _logger.LogInformation(Hash);
                _databasecontext.Users.Add(user);
                await _databasecontext.SaveChangesAsync();
                _logger.LogInformation("signup successful");
            Console.WriteLine(signup.Name, signup.UserEmail, signup.Mobile, signup.Password, signup.Age,signup.UserName);
            return Created("signup successful",user);

            }catch(Exception ex)
            {
                if (ex.InnerException != null)
                {
                    var innerExceptionMessage = ex.InnerException.Message;
                    // Log or print the inner exception message for details
                    return StatusCode(500, innerExceptionMessage);
                }
                _logger.LogError(ex.ToString());
               return StatusCode(500, ex.Message);
            }
        }
    }
}

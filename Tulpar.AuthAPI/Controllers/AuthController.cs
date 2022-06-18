using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Tulpar.AuthAPI.Helper.Dtos;

namespace Tulpar.AuthAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public AuthController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost("Login")]
        public IActionResult Login(LoginDto model)
        {
            TokenHandler._configuration = _configuration;
            if (model.Email == "emretopal07@gmail.com" && model.Password == "emretopal07@gmail.com")
            {
                return Ok(TokenHandler.CreateAccessToken());
            }
            dynamic result = new System.Dynamic.ExpandoObject();
            result.message = "Wrong username and password";
            return BadRequest(result);

        }
    }

}

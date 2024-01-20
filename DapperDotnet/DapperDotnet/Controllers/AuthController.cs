using Data.DTO;
using Data.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DapperDotnet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IauthRepo _authRepo;

        public AuthController(IauthRepo authRepo) {
            _authRepo = authRepo;

        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser(UserDto user)
        {
            var response = await _authRepo.register(user);
            return Ok(response);
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginUser([FromForm]UserDto user)
        {
            var response = await _authRepo.login(user);
            if(response!=null)return Ok(response);
            else return BadRequest(false);
        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PMQuanLy.Models;
using PMQuanLy.Service;

namespace PMQuanLy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] User user)
        {
            var LoginUserCheck = _userService.Authenticate(user.Email, user.Password);

            if (LoginUserCheck == null)
                return BadRequest(new { message = "Email hoặc mật khẩu không chính xác." });
            return Ok(LoginUserCheck);
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] User user)
        {
            var registeredUser = _userService.Register(user.Email, user.Password);

            if (registeredUser == null)
                return BadRequest(new { message = "Email đã tồn tại." });
            return Ok(registeredUser);
        }
    }
}

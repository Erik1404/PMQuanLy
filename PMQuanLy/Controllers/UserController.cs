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
        public IActionResult Login([FromBody] User loginDTO)
        {
            // Thực hiện xác minh danh tính với loginDTO.Email và loginDTO.Password
            var user = _userService.Login(loginDTO.Email, loginDTO.Password);

            if (user == null)
            {
                return BadRequest(new { message = "Email hoặc mật khẩu không chính xác." });
            }

            // Xác minh thành công, trả về thông điệp và dữ liệu đăng nhập
            string roleMessage = _userService.CheckRoleUser(user.Role);

            var response = new
            {
                message = "Đăng nhập thành công.",
                user = new
                {
                    Email = user.Email,
                    Role = user.Role,
                    // Thêm các trường khác mà bạn muốn hiển thị
                },
                role = roleMessage
            };

            return Ok(response);
        }




        [HttpPost("register")]
        public IActionResult Register([FromBody] User newUser)
        {
            var registeredUser = _userService.Register(newUser);

            if (registeredUser == null)
            {
                return BadRequest(new { message = "Email đã tồn tại." });
            }

            return Ok(registeredUser);
        }


        [HttpGet("user-action")]
        public IActionResult UserAction()
        {
            // Lấy email của người dùng hiện tại, ví dụ User.Identity.Name
            string userEmail = User.Identity.Name;

            // Gọi phương thức CheckRoleUser để kiểm tra vai trò
            string roleMessage = _userService.CheckRoleUser(userEmail);

            return Ok(roleMessage);
        }
    }
}

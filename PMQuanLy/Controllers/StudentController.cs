using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PMQuanLy.Data;
using PMQuanLy.Models;
using PMQuanLy.Service;

namespace PMQuanLy.Controllers
{
    //Controllers >> Create API Control
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        /* [HttpGet]
         public async Task<ActionResult<List<Student>>> GetAllStudents()
         {
             var students = await _studentService.GetAllStudents();
             return Ok(students);
         }*/


        [HttpPost("login/student")]
        public IActionResult LoginStd(string Email, string Password)
        {
            // Thực hiện xác minh danh tính với Email và Password
            var std = _studentService.LoginForStudent(Email, Password);

            if (std == null)
            {
                return BadRequest(new { message = "Email hoặc mật khẩu không chính xác." });
            }
            // Đăng nhập thành công
            string welcomeMessage = "Xin chào " + std.FirstName +" "+ std.LastName;

            return Ok(new { message = "Đăng nhập thành công\r\n" + welcomeMessage });
        }

        [HttpPost("register/Std")]
        public IActionResult Register([FromBody] Student newStd)
        {
            var registeredUser = _studentService.RegisterForStudent(newStd);

            if (registeredUser == null)
            {
                return BadRequest(new { message = "Email đã tồn tại." });
            }

            return Ok(registeredUser);
        }


        [HttpDelete("{studentId}")]
        public async Task<ActionResult> DeleteStudent(int studentId)
        {
            var deleted = await _studentService.DeleteStudent(studentId);
            if (deleted)
            {
                return Ok(new { message = "Delete success" });
            }
            else
            {
                return NotFound(new { message = "Not found student" });
            }
        }

        [HttpPut("{studentId}")]
        public async Task<ActionResult> UpdateStudent(int studentId, [FromBody] Student student)
        {
            if (studentId != student.StudentId)
                return BadRequest(new { message = "Data something wrong" });

            var updated = await _studentService.UpdateStudent(student);
            if (updated)
            {
                return Ok(new { message = "Update information success" });
            }
            else
            {
                return NotFound(new { message = "Not found Student" });
            }
        }
    }
}

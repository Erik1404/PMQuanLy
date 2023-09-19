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
        [HttpGet]
        public async Task<ActionResult<List<Student>>> GetAllStudents()
        {
            var students = await _studentService.GetAllStudents();
            return Ok(students);
        }

        [HttpPost]
        public async Task<ActionResult<Student>> AddStudent(Student student)
        {
            var addedStudent = await _studentService.AddStudent(student);
            if (addedStudent != null)
            {
                return Ok(new { message = "Thêm sinh viên thành công", student = addedStudent });
            }
            else
            {
                return BadRequest(new { message = "Thêm sinh viên thất bại" });
            }
        }

        [HttpDelete("{studentId}")]
        public async Task<ActionResult> DeleteStudent(int studentId)
        {
            var deleted = await _studentService.DeleteStudent(studentId);
            if (deleted)
            {
                return Ok(new { message = "Xóa sinh viên thành công" });
            }
            else
            {
                return NotFound(new { message = "Không tìm thấy sinh viên" });
            }
        }

        [HttpPut("{studentId}")]
        public async Task<ActionResult> UpdateStudent(int studentId, Student student)
        {
            if (studentId != student.StudentId)
                return BadRequest(new { message = "Dữ liệu không hợp lệ" });

            var updated = await _studentService.UpdateStudent(student);
            if (updated)
            {
                return Ok(new { message = "Cập nhật sinh viên thành công" });
            }
            else
            {
                return NotFound(new { message = "Không tìm thấy sinh viên" });
            }
        }
    }
}

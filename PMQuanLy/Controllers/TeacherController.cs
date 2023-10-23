using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PMQuanLy.Interface;
using PMQuanLy.Models;

namespace PMQuanLy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        private readonly ITeacherService _teacherService;

        public TeacherController(ITeacherService teacherService)
        {
            _teacherService = teacherService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Teacher>>> GetAllTeachers()
        {
            var Teachers = await _teacherService.GetAllTeachers();
            return Ok(Teachers);
        }


        [HttpDelete("{TeacherId}")]
        public async Task<ActionResult> DeleteTeacher(int TeacherId)
        {
            var deleted = await _teacherService.DeleteTeacher(TeacherId);
            if (deleted)
            {
                return Ok(new { message = "Delete success" });
            }
            else
            {
                return NotFound(new { message = "Not found Teacher" });
            }
        }

        [HttpPut("update/{TeacherId}")]
        public async Task<ActionResult> UpdateTeacher(int userId, [FromBody] Teacher Teacher)
        {
            // Kiểm tra xem userId có trùng khớp với Teacher.UserId không
            if (userId != Teacher.UserId)
            {
                return BadRequest(new { message = "Dữ liệu không hợp lệ" });
            }

            try
            {
                var updated = await _teacherService.UpdateTeacher(Teacher);

                if (updated)
                {
                    return Ok(new { message = "Cập nhật thông tin thành công" });
                }
                else
                {
                    return NotFound(new { message = "Không tìm thấy thông tin giáo viên" });
                }
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (DbUpdateException)
            {
                return BadRequest(new { message = "Có lỗi xảy ra " });
            }
        }

        [HttpGet("{teacherId}/courses-and-students")]
        public async Task<IActionResult> GetCoursesAndStudentsByTeacherId(int teacherId)
        {
            try
            {
                var coursesAndStudents = await _teacherService.GetCoursesAndStudentsByTeacherId(teacherId);

                if (coursesAndStudents == null || !coursesAndStudents.Any())
                {
                    // Trường hợp không tìm thấy dữ liệu, trả về lỗi 404 và thông báo.
                    return NotFound("Giáo viên này có thể chưa được phân công môn giảng dạy nào");
                }

                return Ok(coursesAndStudents);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

    }
}

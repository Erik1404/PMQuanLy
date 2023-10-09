using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PMQuanLy.Models;
using PMQuanLy.Service;

namespace PMQuanLy.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CourseRegistrationController : ControllerBase
    {
        private readonly ICourseRegistrationService _CourseRegistrationService;

        public CourseRegistrationController(CourseRegistrationService CourseRegistrationService)
        {
            _CourseRegistrationService = CourseRegistrationService;
        }


        [HttpGet]
        public async Task<ActionResult<List<CourseRegistration>>> GetAllCourseRegistrations()
        {
            var CourseRegistrations = await _CourseRegistrationService.GetAllCourseRegistrations();
            return Ok(CourseRegistrations);
        }

        [HttpDelete("{CourseRegistrationId}")]
        public async Task<ActionResult> DeleteCourseRegistration(int CourseRegistrationId)
        {
            var deleted = await _CourseRegistrationService.DeleteCourseRegistration(CourseRegistrationId);
            if (deleted)
            {
                return Ok(new { message = "Delete success" });
            }
            else
            {
                return NotFound(new { message = "Not found CourseRegistration" });
            }
        }

        [HttpPut("update/{CourseRegistrationId}")]
        public async Task<ActionResult> UpdateCourseRegistration(int CourseRegistrationId, [FromBody] CourseRegistration CourseRegistration)
        {
            if (CourseRegistrationId != CourseRegistration.RegistrationId)
            {
                return BadRequest(new { message = "Dữ liệu không hợp lệ" });
            }

            try
            {
                var updated = await _CourseRegistrationService.DeleteCourseRegistration(CourseRegistrationId);

                if (updated)
                {
                    return Ok(new { message = "Cập nhật thông tin thành công" });
                }
                else
                {
                    return NotFound(new { message = "Không tìm thấy thông tin môn học" });
                }
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (DbUpdateException)
            {
                return BadRequest(new { message = "Có lỗi xảy ra khi cập nhật thông tin" });
            }
        }


        // Đếm số lượng mở lớp

        [HttpGet("CountStudent/{CourseId}")]
        public async Task<ActionResult> CountStudentInCourse(int CourseId)
        {
            var count = await _CourseRegistrationService.CountStudentInCourse(CourseId);
            return Ok(count);
        }

        // danh sách học sinh trong lớp

        [HttpGet("GetList/{CourseId}")]
        public async Task<ActionResult> ListStudentInCourse(int CourseId)
        {
            var students = await _CourseRegistrationService.ListStudentInCourse(CourseId);
            return Ok(students);
        }
        


        // Tìm học sinh >> học sinh đang học lớp gì
        [HttpGet("{StudentId}")]
        public async Task<ActionResult> FindCoureByStudentId(int StudentId)
        {
            var course = await _CourseRegistrationService.FindCoureByStudentId(StudentId);
            return Ok(course);
        }
    }
}

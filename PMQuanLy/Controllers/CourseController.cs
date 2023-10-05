using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PMQuanLy.Models;
using PMQuanLy.Service;

namespace PMQuanLy.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CourseController : ControllerBase
    {
        private readonly ICourseService _CourseService;

        public CourseController(CourseService CourseService)
        {
            _CourseService = CourseService;
        }


        [HttpGet]
        public async Task<ActionResult<List<Course>>> GetAllCourses()
        {
            var Courses = await _CourseService.GetAllCourses();
            return Ok(Courses);
        }

        [HttpDelete("{CourseId}")]
        public async Task<ActionResult> DeleteStudent(int CourseId)
        {
            var deleted = await _CourseService.DeleteCourse(CourseId);
            if (deleted)
            {
                return Ok(new { message = "Delete success" });
            }
            else
            {
                return NotFound(new { message = "Not found Course" });
            }
        }

        [HttpPut("update/{CourseId}")]
        public async Task<ActionResult> UpdateStudent(int CourseId, [FromBody] Course Course)
        {
            if (CourseId != Course.CourseId)
            {
                return BadRequest(new { message = "Dữ liệu không hợp lệ" });
            }

            try
            {
                var updated = await _CourseService.DeleteCourse(CourseId);

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
    }
}

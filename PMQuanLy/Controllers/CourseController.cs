using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PMQuanLy.Interface;
using PMQuanLy.Models;

namespace PMQuanLy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ICourseService _CourseService;
        public CourseController(ICourseService CourseService)
        {
            _CourseService = CourseService;
        }


        [HttpGet("All Courses")]
        public async Task<ActionResult<List<Course>>> GetAllCourses()
        {
            var Courses = await _CourseService.GetAllCourses();
            return Ok(Courses);
        }

        [HttpGet("Search Course")]
        public IActionResult SearchCourses(string keyword)
        {
            var Courses = _CourseService.SearchCourse(keyword);
            return Ok(Courses);
        }

        [HttpPost("Add Course")]
        public async Task<ActionResult<Course>> AddCourse(Course Course)
        {
            var add = await _CourseService.AddCourse(Course);
            if (add != null)
            {
                return Ok(new { message = "Thêm môn thành công", Course = add });
            }
            else
            {
                return BadRequest(new { message = "Thêm môn thất bại" });
            }
        }

        [HttpDelete("Delete Course")]
        public async Task<ActionResult> DeleteCourse(int CourseId)
        {
            var deleted = await _CourseService.DeleteCourse(CourseId);
            if (deleted)
            {
                return Ok(new { message = "Xóa môn thành công" });
            }
            else
            {
                return NotFound(new { message = "Không tìm thấy môn" });
            }
        }


        [HttpPut("Update Course")]
        public async Task<ActionResult> UpdateCourse(int CourseId, Course Course)
        {
            if (CourseId != Course.CourseId)
                return BadRequest(new { message = "Dữ liệu không hợp lệ" });

            var updated = await _CourseService.UpdateCourse(Course);
            if (updated)
            {
                return Ok(new { message = "Cập nhật môn thành công" });
            }
            else
            {
                return NotFound(new { message = "Không tìm thấy môn" });
            }
        }
    }
}

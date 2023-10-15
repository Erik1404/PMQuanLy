using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PMQuanLy.Interface;
using PMQuanLy.Models;

namespace PMQuanLy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseYearController : ControllerBase
    {
        private readonly ICourseYearService _courseYearService;
        public CourseYearController(ICourseYearService CourseYearService)
        {
            _courseYearService = CourseYearService;
        }


        [HttpGet("GetAllCourseYear")]
        public async Task<ActionResult<List<CourseYear>>> GetAllCourseYears()
        {
            try
            {
                var courseYears = await _courseYearService.GetAllCourseYears();
                return Ok(courseYears);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        [HttpGet("Search CourseYear")]
        public IActionResult SearchCourseYears(string keyword)
        {
            var CourseYears = _courseYearService.SearchCourseYear(keyword);
            return Ok(CourseYears);
        }

        [HttpPost("Add CourseYear")]
        public async Task<ActionResult<CourseYear>> AddCourseYear(CourseYear CourseYear)
        {
            var add = await _courseYearService.AddCourseYear(CourseYear);
            if (add != null)
            {
                return Ok(new { message = "Thêm khóa mới thành công", CourseYear = add });
            }
            else
            {
                return BadRequest(new { message = "Thêm khóa mới thất bại" });
            }
        }

        [HttpDelete("Delete CourseYear")]
        public async Task<ActionResult> DeleteCourseYear(int CourseYearId)
        {
            var deleted = await _courseYearService.DeleteCourseYear(CourseYearId);
            if (deleted)
            {
                return Ok(new { message = "Xóa khóa mới thành công" });
            }
            else
            {
                return NotFound(new { message = "Không tìm thấy data" });
            }
        }


        [HttpPut("Update CourseYear")]
        public async Task<ActionResult> UpdateCourseYear(int CourseYearId, CourseYear CourseYear)
        {
            if (CourseYearId != CourseYear.CourseYearId)
                return BadRequest(new { message = "Dữ liệu không hợp lệ" });

            var updated = await _courseYearService.UpdateCourseYear(CourseYear);
            if (updated)
            {
                return Ok(new { message = "Cập nhật khóa mới thành công" });
            }
            else
            {
                return NotFound(new { message = "Không tìm thấy dữ liệu" });
            }
        }
    }
}

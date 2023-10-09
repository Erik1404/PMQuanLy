using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PMQuanLy.Models;
using PMQuanLy.Service;

namespace PMQuanLy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseRegistrationController : ControllerBase
    {
        private readonly ICourseRegistrationService _CourseRegistrationService;
        public CourseRegistrationController(ICourseRegistrationService CourseRegistrationService)
        {
            _CourseRegistrationService = CourseRegistrationService;
        }


        [HttpGet("All CourseRegistrations")]
        public async Task<ActionResult<List<CourseRegistration>>> GetAllCourseRegistrations()
        {
            var CourseRegistrations = await _CourseRegistrationService.GetAllCourseRegistrations();
            return Ok(CourseRegistrations);
        }

       

        [HttpPost("Add CourseRegistration")]
        public async Task<ActionResult<CourseRegistration>> AddCourseRegistration(CourseRegistration CourseRegistration)
        {
            var add = await _CourseRegistrationService.AddCourseRegistration(CourseRegistration);
            if (add != null)
            {
                return Ok(new { message = "Thêm  thành công", CourseRegistration = add });
            }
            else
            {
                return BadRequest(new { message = "Thêm thất bại" });
            }
        }

        [HttpDelete("Delete CourseRegistration")]
        public async Task<ActionResult> DeleteCourseRegistration(int CourseRegistrationId)
        {
            var deleted = await _CourseRegistrationService.DeleteCourseRegistration(CourseRegistrationId);
            if (deleted)
            {
                return Ok(new { message = "Xóa thành công" });
            }
            else
            {
                return NotFound(new { message = "Không tìm thấy dữ liệu" });
            }
        }


        [HttpPut("Update CourseRegistration")]
        public async Task<ActionResult> UpdateCourseRegistration(int CourseRegistrationId, CourseRegistration CourseRegistration)
        {
            if (CourseRegistrationId != CourseRegistration.CourseRegistrationId)
                return BadRequest(new { message = "Dữ liệu không hợp lệ" });

            var updated = await _CourseRegistrationService.UpdateCourseRegistration(CourseRegistration);
            if (updated)
            {
                return Ok(new { message = "Cập nhật thành công" });
            }
            else
            {
                return NotFound(new { message = "Không tìm thấy dữ liệu" });
            }
        }
    }
}

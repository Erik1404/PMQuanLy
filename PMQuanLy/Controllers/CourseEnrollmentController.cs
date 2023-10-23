using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PMQuanLy.Interface;
using PMQuanLy.Models;

namespace PMQuanLy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseEnrollmentController : ControllerBase
    {
        private readonly ICourseEnrollmentService _courseEnrollmentService;

        public CourseEnrollmentController(ICourseEnrollmentService courseEnrollmentService)
        {
            _courseEnrollmentService = courseEnrollmentService;
        }

        [HttpGet("Get Data CourseEnrollment")]
        public async Task<IActionResult> GetClosedCourseEnrollments()
        {
            var closedCourseEnrollments = await _courseEnrollmentService.GetAllCourseEnrollments();
            return Ok(closedCourseEnrollments);
        }

        [HttpPost("CreateStudentScore")]
        public async Task<IActionResult> CreateStudentScore(int courseEnrollmentId, string scoreName, double scoreValue, double scoreCoefficient)
        {
            try
            {
                await _courseEnrollmentService.CreateStudentScore(courseEnrollmentId, scoreName, scoreValue, scoreCoefficient);
                return Ok("Nhập điểm cho học viên hoàn tất");
            }
            catch (Exception ex)
            {
                return BadRequest("Lỗi : " + ex.Message);
            }
        }
    }
}

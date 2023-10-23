using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PMQuanLy.Interface;
using PMQuanLy.Models;

namespace PMQuanLy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseRegistrationController : ControllerBase
    {
        private readonly ICourseRegistrationService _courseRegistrationService;
        private readonly ICourseEnrollmentService _courseEnrollmentService;

        public CourseRegistrationController(ICourseRegistrationService courseRegistrationService, ICourseEnrollmentService courseEnrollmentService)
        {
            _courseRegistrationService = courseRegistrationService;
            _courseEnrollmentService = courseEnrollmentService;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllCourseRegistrations()
        {
            var courseRegistrations = await _courseRegistrationService.GetAllCourseRegistrations();
            return Ok(courseRegistrations);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCourseRegistrationById(int id)
        {
            var courseRegistration = await _courseRegistrationService.GetCourseRegistrationById(id);

            if (courseRegistration == null)
            {
                return NotFound();
            }

            return Ok(courseRegistration);
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> RegisterStudentForCourse(int studentId, int courseId)
        {
            var result = await _courseRegistrationService.RegisterStudentForCourse(studentId, courseId);

            if (result != null)
            {
                return Ok(result);
            }

            return BadRequest("Failed to register the student for the course.");
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> UnregisterStudentFromCourse(int id)
        {
            var success = await _courseRegistrationService.UnregisterStudentFromCourse(id);

            if (success)
            {
                return Ok(new { message = "Xóa đăng ký thành công" });
            }

            return NotFound(new { message = "Không tìm thấy đăng ký hoặc xóa không thành công" });
        }


        [HttpGet("GetCourseRegistrationsForStudent/{studentId}")]
        public async Task<IActionResult> GetCourseRegistrationsForStudent(int studentId)
        {
            var courseRegistrations = await _courseRegistrationService.GetCourseRegistrationsForStudent(studentId);
            return Ok(courseRegistrations);
        }

        [HttpGet("GetCourseRegistrationsForCourse/{courseId}")]
        public async Task<IActionResult> GetCourseRegistrationsForCourse(int courseId)
        {
            var courseRegistrations = await _courseRegistrationService.GetCourseRegistrationsForCourse(courseId);
            return Ok(courseRegistrations);
        }


        [HttpGet("Count Student/{CourseId}")]
        public async Task<ActionResult> CountStudentInCourse(int CourseId)
        {
            var count = await _courseRegistrationService.CountStudentInCourse(CourseId);
            return Ok(count);
        }

    }
}

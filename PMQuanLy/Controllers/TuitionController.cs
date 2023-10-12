using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PMQuanLy.Models;
using PMQuanLy.Service;

namespace PMQuanLy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TuitionController : ControllerBase
    {
        private readonly ITuitionService _tuitionService;
        private readonly ICourseRegistrationService _courseRegistrationService;
        private readonly ICourseService _courseService;
        private readonly IStudentService _studentService;

        public TuitionController(ITuitionService tuitionService,ICourseRegistrationService courseRegistrationService, IStudentService studentService, ICourseService courseService)
        {
            _tuitionService = tuitionService;
            _courseRegistrationService = courseRegistrationService;
            _studentService = studentService;
            _courseService = courseService;
        }


        /* [HttpGet]
         public async Task<IActionResult> GetAllTuitions()
         {
             var tuitions = await _tuitionService.GetAllTuitions();
             return Ok(tuitions);
         }

         [HttpGet("{id}")]
         public async Task<IActionResult> GetTuitionById(int id)
         {
             var tuition = await _tuitionService.GetTuitionById(id);
             if (tuition == null)
             {
                 return NotFound();
             }
             return Ok(tuition);
         }

         [HttpGet("student/{studentId}")]
         public async Task<IActionResult> GetTuitionsByStudentId(int studentId)
         {
             var tuitions = await _tuitionService.GetTuitionsByStudentId(studentId);
             return Ok(tuitions);
         }

         [HttpPost]
         public async Task<IActionResult> AddTuition([FromBody] Tuition tuition)
         {
             if (tuition == null)
             {
                 return BadRequest();
             }

             var addedTuition = await _tuitionService.AddTuition(tuition);
             return CreatedAtAction(nameof(GetTuitionById), new { id = addedTuition.TuitionId }, addedTuition);
         }

         [HttpPut("{id}")]
         public async Task<IActionResult> UpdateTuition(int id, [FromBody] Tuition tuition)
         {
             if (tuition == null || id != tuition.TuitionId)
             {
                 return BadRequest();
             }

             var updated = await _tuitionService.UpdateTuition(tuition);
             if (!updated)
             {
                 return NotFound();
             }

             return Ok(tuition);
         }

         [HttpDelete("{id}")]
         public async Task<IActionResult> DeleteTuition(int id)
         {
             var deleted = await _tuitionService.DeleteTuition(id);
             if (!deleted)
             {
                 return NotFound();
             }
             return NoContent();
         }*/

        [HttpGet("total/{studentId}")]
        public async Task<IActionResult> GetTotalTuition(int studentId)
        {
            var student = await _studentService.GetStudentById(studentId);

            if (student == null)
            {
                return NotFound("Học sinh không tồn tại");
            }

            var totalTuition = await _tuitionService.CalculateTotalTuitionForStudent(studentId);

            // Lấy danh sách các khóa học đã đăng ký bởi học sinh
            var courseRegistrations = await _courseRegistrationService.GetCourseRegistrationsForStudent(studentId);
            var registeredCourses = new List<Course>();

            foreach (var registration in courseRegistrations)
            {
                var course = await _courseService.GetCourseById(registration.CourseId);
                if (course != null)
                {
                    registeredCourses.Add(course);
                }
            }

            var response = new
            {
                TotalTuition = totalTuition,
                RegisteredCourses = registeredCourses
            };

            return Ok(response);
        }

    }
}

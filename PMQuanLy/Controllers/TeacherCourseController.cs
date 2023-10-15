using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PMQuanLy.Interface;

namespace PMQuanLy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherCourseController : ControllerBase
    {
        private readonly ITeacherCourseService _teacherCourseService;

        public TeacherCourseController(ITeacherCourseService teacherCourseService)
        {
            _teacherCourseService = teacherCourseService;
        }


        [HttpPost("AddCourseForTeacher")]
        public async Task<IActionResult> AddCourseForTeacher(int teacherId, int courseId)
        {
            try
            {
                // Gọi phương thức từ dịch vụ _teacherCourseService để thêm khóa học vào danh sách khóa học của giáo viên
                var result = await _teacherCourseService.AddCourseForTeacher(teacherId, courseId);

                if (result)
                {
                    return Ok(new { message = "Thêm khóa học vào danh sách khóa học của giáo viên thành công" });
                }
                else
                {
                    return BadRequest(new { message = "Không thể thêm khóa học vào danh sách khóa học của giáo viên" });
                }
            }
            catch (Exception)
            {
                // Xử lý lỗi
                return StatusCode(500, new { message = "Lỗi trong quá trình thêm khóa học vào danh sách khóa học của giáo viên" });
            }
        }


        [HttpPost("AddTeacherToCourse")]
        public async Task<IActionResult> AddTeacherToCourse(int courseId, int teacherId)
        {
            try
            {
                // Gọi phương thức từ dịch vụ _teacherCourseService để thêm giáo viên vào khóa học
                var result = await _teacherCourseService.AddTeacherToCourse(courseId, teacherId);

                if (result)
                {
                    return Ok(new { message = "Thêm giáo viên vào khóa học thành công" });
                }
                else
                {
                    return BadRequest(new { message = "Không thể thêm giáo viên vào khóa học" });
                }
            }
            catch (Exception)
            {
                // Xử lý lỗi
                return BadRequest(new { message = "Lỗi trong quá trình thêm giáo viên vào khóa học" });
            }
        }


        [HttpGet("GetCoursesForTeacher/{teacherId}")]
        public async Task<IActionResult> GetCoursesForTeacher(int teacherId)
        {
            try
            {
                // Gọi phương thức từ dịch vụ _teacherCourseService để lấy danh sách khóa học của giáo viên
                var courses = await _teacherCourseService.GetCoursesForTeacher(teacherId);

                if (courses != null)
                {
                    return Ok(courses);
                }
                else
                {
                    return NotFound(new { message = "Không tìm thấy khóa học cho giáo viên này" });
                }
            }
            catch (Exception)
            {
                // Xử lý lỗi
                return BadRequest(new { message = "Lỗi trong quá trình lấy danh sách khóa học của giáo viên" });
            }
        }

        [HttpGet("GetTeachersForCourse/{courseId}")]
        public async Task<IActionResult> GetTeachersForCourse(int courseId)
        {
            try
            {
                // Gọi phương thức từ dịch vụ _teacherCourseService để lấy danh sách giáo viên của khóa học
                var teachers = await _teacherCourseService.GetTeachersForCourse(courseId);

                if (teachers != null)
                {
                    return Ok(teachers);
                }
                else
                {
                    return NotFound(new { message = "Không tìm thấy giáo viên cho khóa học này" });
                }
            }
            catch (Exception)
            {
                // Xử lý lỗi
                return BadRequest(new { message = "Lỗi trong quá trình lấy danh sách giáo viên của khóa học" });
            }
        }


        [HttpDelete("RemoveTeacherFromCourse/{courseId}/{teacherId}")]
        public async Task<IActionResult> RemoveTeacherFromCourse(int courseId, int teacherId)
        {
            try
            {
                // Gọi phương thức từ dịch vụ _teacherCourseService để xóa giáo viên khỏi khóa học
                var result = await _teacherCourseService.RemoveTeacherFromCourse(courseId, teacherId);

                if (result)
                {
                    return Ok(new { message = "Xóa giáo viên khỏi khóa học thành công" });
                }
                else
                {
                    return BadRequest(new { message = "Không thể xóa giáo viên khỏi khóa học" });
                }
            }
            catch (Exception)
            {
                // Xử lý lỗi
                return BadRequest(new { message = "Lỗi trong quá trình xóa giáo viên khỏi khóa học" });
            }
        }


        [HttpDelete("RemoveCourseFromTeacher/{teacherId}/{courseId}")]
        public async Task<IActionResult> RemoveCourseFromTeacher(int teacherId, int courseId)
        {
            try
            {
                // Gọi phương thức từ dịch vụ _teacherCourseService để xóa khóa học khỏi danh sách khóa học của giáo viên
                var result = await _teacherCourseService.RemoveCourseFromTeacher(teacherId, courseId);

                if (result)
                {
                    return Ok(new { message = "Xóa khóa học khỏi danh sách khóa học của giáo viên thành công" });
                }
                else
                {
                    return BadRequest(new { message = "Không thể xóa khóa học khỏi danh sách khóa học của giáo viên" });
                }
            }
            catch (Exception)
            {
                // Xử lý lỗi
                return StatusCode(500, new { message = "Lỗi trong quá trình xóa khóa học khỏi danh sách khóa học của giáo viên" });
            }
        }
    }
}

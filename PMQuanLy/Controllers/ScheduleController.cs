using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PMQuanLy.Models;
using PMQuanLy.Service;

namespace PMQuanLy.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ScheduleController : ControllerBase
    {
        private readonly IScheduleService _ScheduleService;

        public ScheduleController(ScheduleService ScheduleService)
        {
            _ScheduleService = ScheduleService;
        }


        [HttpGet]
        public async Task<ActionResult<List<Schedule>>> GetAllSchedules()
        {
            var Schedules = await _ScheduleService.GetAllSchedules();
            return Ok(Schedules);
        }

        [HttpDelete("deleteSchedule/{ScheduleId}")]
        public async Task<ActionResult> DeleteSchedule(int ScheduleId)
        {
            var deleted = await _ScheduleService.DeleteSchedule(ScheduleId);
            if (deleted)
            {
                return Ok(new { message = "Delete success" });
            }
            else
            {
                return NotFound(new { message = "Not found Schedule" });
            }
        }

        [HttpPut("update/{ScheduleId}")]
        public async Task<ActionResult> UpdateSchedule(int ScheduleId, [FromBody] Schedule Schedule)
        {
            if (ScheduleId != Schedule.ScheduleId)
            {
                return BadRequest(new { message = "Dữ liệu không hợp lệ" });
            }

            try
            {
                var updated = await _ScheduleService.DeleteSchedule(ScheduleId);

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


        [HttpGet("Count/{TeacherId}")]
        public async Task<ActionResult> CountCourseInTeacherId(int TeacherId)
        {
            var count = await _ScheduleService.CountCourseInTeacherId(TeacherId);
            return Ok(count);
        }

        [HttpGet("GetList/{TeacherId}")]
        public async Task<ActionResult> ListCourseInTeacher(int TeacherId)
        {
            var course = await _ScheduleService.ListCourseInTeacher(TeacherId);
            return Ok(course);
        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PMQuanLy.Models;
using PMQuanLy.Service;

namespace PMQuanLy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScheduleController : ControllerBase
    {
        private readonly IScheduleService _ScheduleService;
        public ScheduleController(IScheduleService ScheduleService)
        {
            _ScheduleService = ScheduleService;
        }


        [HttpGet("All Schedules")]
        public async Task<ActionResult<List<Schedule>>> GetAllSchedules()
        {
            var Schedules = await _ScheduleService.GetAllSchedules();
            return Ok(Schedules);
        }

      

        [HttpPost("Add Schedule")]
        public async Task<ActionResult<Schedule>> AddSchedule(Schedule Schedule)
        {
            var add = await _ScheduleService.AddSchedule(Schedule);
            if (add != null)
            {
                return Ok(new { message = "Thêm thành công", Schedule = add });
            }
            else
            {
                return BadRequest(new { message = "Thêm  thất bại" });
            }
        }

        [HttpDelete("Delete Schedule")]
        public async Task<ActionResult> DeleteSchedule(int ScheduleId)
        {
            var deleted = await _ScheduleService.DeleteSchedule(ScheduleId);
            if (deleted)
            {
                return Ok(new { message = "Xóa thành công" });
            }
            else
            {
                return NotFound(new { message = "Không tìm thấy" });
            }
        }


        [HttpPut("Update Schedule")]
        public async Task<ActionResult> UpdateSchedule(int ScheduleId, Schedule Schedule)
        {
            if (ScheduleId != Schedule.ScheduleId)
                return BadRequest(new { message = "Dữ liệu không hợp lệ" });

            var updated = await _ScheduleService.UpdateSchedule(Schedule);
            if (updated)
            {
                return Ok(new { message = "Cập nhật thành công" });
            }
            else
            {
                return NotFound(new { message = "Không tìm thấy" });
            }
        }
    }
}

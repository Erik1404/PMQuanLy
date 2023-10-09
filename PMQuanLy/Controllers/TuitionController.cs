using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PMQuanLy.Models;
using PMQuanLy.Service;

namespace PMQuanLy.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TuitionController : ControllerBase
    {
        private readonly ITuitionService _TuitionService;

        public TuitionController(TuitionService TuitionService)
        {
            _TuitionService = TuitionService;
        }


        [HttpGet]
        public async Task<ActionResult<List<Tuition>>> GetAllTuitions()
        {
            var Tuitions = await _TuitionService.GetAllTuitions();
            return Ok(Tuitions);
        }

        [HttpDelete("{TuitionId}")]
        public async Task<ActionResult> DeleteTuition(int TuitionId)
        {
            var deleted = await _TuitionService.DeleteTuition(TuitionId);
            if (deleted)
            {
                return Ok(new { message = "Delete success" });
            }
            else
            {
                return NotFound(new { message = "Not found Tuition" });
            }
        }

        [HttpPut("update/{TuitionId}")]
        public async Task<ActionResult> UpdateTuition(int TuitionId, [FromBody] Tuition Tuition)
        {
            if (TuitionId != Tuition.TuitionId)
            {
                return BadRequest(new { message = "Dữ liệu không hợp lệ" });
            }

            try
            {
                var updated = await _TuitionService.DeleteTuition(TuitionId);

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

        [HttpGet("{StudentId}")]
        public async Task<ActionResult> TotalAmount(int StudentId)
        {
            var total = await _TuitionService.ToTalTuitionInStudent(StudentId);
            return Ok(total);
        }
    }
}

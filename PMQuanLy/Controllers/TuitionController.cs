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
        private readonly ITuitionService _TuitionService;
        public TuitionController(ITuitionService TuitionService)
        {
            _TuitionService = TuitionService;
        }


        [HttpGet("All Tuitions")]
        public async Task<ActionResult<List<Tuition>>> GetAllTuitions()
        {
            var Tuitions = await _TuitionService.GetAllTuitions();
            return Ok(Tuitions);
        }



        [HttpPost("Add Tuition")]
        public async Task<ActionResult<Tuition>> AddTuition(Tuition Tuition)
        {
            var add = await _TuitionService.AddTuition(Tuition);
            if (add != null)
            {
                return Ok(new { message = "Thêm thành công", Tuition = add });
            }
            else
            {
                return BadRequest(new { message = "Thêm  thất bại" });
            }
        }

        [HttpDelete("Delete Tuition")]
        public async Task<ActionResult> DeleteTuition(int TuitionId)
        {
            var deleted = await _TuitionService.DeleteTuition(TuitionId);
            if (deleted)
            {
                return Ok(new { message = "Xóa thành công" });
            }
            else
            {
                return NotFound(new { message = "Không tìm thấy" });
            }
        }


        [HttpPut("Update Tuition")]
        public async Task<ActionResult> UpdateTuition(int TuitionId, Tuition Tuition)
        {
            if (TuitionId != Tuition.TuitionId)
                return BadRequest(new { message = "Dữ liệu không hợp lệ" });

            var updated = await _TuitionService.UpdateTuition(Tuition);
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

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PMQuanLy.Interface;
using PMQuanLy.Models;
using PMQuanLy.Service;

namespace PMQuanLy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectController : ControllerBase
    {
        private readonly ISubjectService _subjectService;
        public SubjectController(ISubjectService subjectService)
        {
            _subjectService = subjectService;
        }


        [HttpGet("All Subjects")]
        public async Task<ActionResult<List<Subject>>> GetAllSubjects()
        {
            try
            {
                var subjects = await _subjectService.GetAllSubjects();
                return Ok(subjects);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("Search Subject")]
        public IActionResult SearchSubjects(string keyword)
        {
            var subjects = _subjectService.SearchSubject(keyword);
            return Ok(subjects);
        }

        [HttpPost("Add Subject")]
        public async Task<ActionResult<Subject>> AddSubject(Subject subject)
        {
            var add = await _subjectService.AddSubject(subject);
            if (add != null)
            {
                return Ok(new { message = "Thêm môn thành công", subject = add });
            }
            else
            {
                return BadRequest(new { message = "Thêm môn thất bại" });
            }
        }

        [HttpDelete("Delete Subject")]
        public async Task<ActionResult> DeleteSubject(int subjectId)
        {
            var deleted = await _subjectService.DeleteSubject(subjectId);
            if (deleted)
            {
                return Ok(new { message = "Xóa môn thành công" });
            }
            else
            {
                return NotFound(new { message = "Không tìm thấy môn" });
            }
        }


        [HttpPut("Update Subject")]
        public async Task<ActionResult> UpdateSubject(int subjectId, Subject subject)
        {
            if (subjectId != subject.SubjectId)
                return BadRequest(new { message = "Dữ liệu không hợp lệ" });

            var updated = await _subjectService.UpdateSubject(subject);
            if (updated)
            {
                return Ok(new { message = "Cập nhật môn thành công" });
            }
            else
            {
                return NotFound(new { message = "Không tìm thấy môn" });
            }
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PMQuanLy.Models;
using PMQuanLy.Service;

namespace PMQuanLy.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SubjectController : ControllerBase
    {
        private readonly ISubjectService _subjectService;

        public SubjectController(SubjectService subjectService)
        {
            _subjectService = subjectService;
        }


        [HttpGet]
        public async Task<ActionResult<List<Subject>>> GetAllSubjects()
        {
            var subjects = await _subjectService.GetAllSubjects();
            return Ok(subjects);
        }


        //Search - Get with Name
        [HttpGet("api/SearchSubject")]
        public IActionResult SearchSubject(string keyword)
        {
            var subjects = _subjectService.SearchSubjects(keyword);
            return Ok(subjects);
        }


        [HttpPost("api/AddSubject")] 
        public async Task<ActionResult<Subject>> AddSubject(Subject subject)
        {
            var addsubject = await _subjectService.AddSubject(subject);
            if (addsubject != null)
            {
                return Ok(new { message = "Thêm môn học mới thành công", subject = addsubject });
            }
            else
            {
                return BadRequest(new { message = "Có lỗi" });
            }
        }

        [HttpDelete("{subjectId}")]
        public async Task<ActionResult> DeleteSubject(int subjectId)
        {
            var deleted = await _subjectService.DeleteSubject(subjectId);
            if (deleted)
            {
                return Ok(new { message = "Delete success" });
            }
            else
            {
                return NotFound(new { message = "Not found subject" });
            }
        }

        [HttpPut("update/{subjectId}")]
        public async Task<ActionResult> UpdateSubject(int subjectId, [FromBody] Subject subject)
        {
            if (subjectId != subject.SubjectId)
            {
                return BadRequest(new { message = "Dữ liệu không hợp lệ" });
            }

            try
            {
                var updated = await _subjectService.DeleteSubject(subjectId);

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
    }
}

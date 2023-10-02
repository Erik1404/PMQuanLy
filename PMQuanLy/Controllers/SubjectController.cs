using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        [HttpGet("api/GetAllSubject")]
        public async Task<ActionResult<List<Subject>>> GetAllSubjects()
        {
            var subjects = await _subjectService.GetAllSubjects();
            return Ok(subjects);
        }


        //Get Subject theo ID
        [HttpGet("api/SearchSubject")]
        public IActionResult SearchSubject(string keyword)
        {
            var teachers = _subjectService.SearchSubjects(keyword);
            return Ok(teachers);
        }

        [HttpPost("api/AddSubject")]
        public async Task<ActionResult<Subject>> AddSubject(Subject subject)
        {
            var addedsubject = await _subjectService.AddSubject(subject);
            if (addedsubject != null)
            {
                return Ok(new { message = "Thêm thành công", subject = addedsubject });
            }
            else
            {
                return BadRequest(new { message = "Có lỗi" });
            }
        }

        [HttpDelete("api/DeleteSubject")]
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

        [HttpPut("api/UpdateSubject")]
        public async Task<ActionResult> UpdateSubject(int subjectId, Subject subject)
        {
            if (subjectId != subject.SubjectId)
                return BadRequest(new { message = "Lỗi dữ liệu" });

            var updated = await _subjectService.UpdateSubject(subject);
            if (updated)
            {
                return Ok(new { message = "Cập nhật thành công" });
            }
            else
            {
                return NotFound(new { message = "Có lỗi" });
            }
        }


    }
}

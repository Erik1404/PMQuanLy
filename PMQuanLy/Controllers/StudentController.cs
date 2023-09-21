using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PMQuanLy.Data;
using PMQuanLy.Models;
using PMQuanLy.Service;

namespace PMQuanLy.Controllers
{
    //Controllers >> Create API Control
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }
        [HttpGet]
        public async Task<ActionResult<List<Student>>> GetAllStudents()
        {
            var students = await _studentService.GetAllStudents();
            return Ok(students);
        }
/*
        [HttpGet("{id}")]
        public IActionResult GetStudentById(int id)
        {
            var student = _studentService.GetStudentById(id);
            if (student == null)
            {
                return NotFound();
            }
            return Ok(student);
        }*/
        [HttpPost]
        public async Task<ActionResult<Student>> AddStudent(Student student)
        {
            var addedStudent = await _studentService.AddStudent(student);
            if (addedStudent != null)
            {
                return Ok(new { message = "Add success", student = addedStudent });
            }
            else
            {
                return BadRequest(new { message = "Add failed" });
            }
        }

        [HttpDelete("{studentId}")]
        public async Task<ActionResult> DeleteStudent(int studentId)
        {
            var deleted = await _studentService.DeleteStudent(studentId);
            if (deleted)
            {
                return Ok(new { message = "Delete success" });
            }
            else
            {
                return NotFound(new { message = "Not found student" });
            }
        }

        [HttpPut("{studentId}")]
        public async Task<ActionResult> UpdateStudent(int studentId, [FromBody] Student student)
        {
            if (studentId != student.StudentId)
                return BadRequest(new { message = "Data something wrong" });

            var updated = await _studentService.UpdateStudent(student);
            if (updated)
            {
                return Ok(new { message = "Update information success" });
            }
            else
            {
                return NotFound(new { message = "Not found Student" });
            }
        }
    }
}

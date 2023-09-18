using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PMQuanLy.Data;
using PMQuanLy.Models;
using PMQuanLy.Service;

namespace PMQuanLy.Controllers
{
    //Controllers >> Create API Control
    [Route("api/[controller]")]
    [ApiController]
    /* public class StudentController : ControllerBase
     {
         private readonly PMQLDbContext _dbContext;

         public StudentController(PMQLDbContext dbContext)
         {
             _dbContext = dbContext;
         }
         // GetAll
         [HttpGet]
         public async Task<ActionResult<IEnumerable<Student>>> GetStudents()
         {
             if(_dbContext.Students == null)
             {
                 return NotFound();
             }
             return await _dbContext.Students.ToListAsync();
         }

         // Search Student with ID
         [HttpGet("{id}")]
         public async Task<ActionResult<Student>> GetStudents(int id)
         {
             if (_dbContext.Students == null)
             {
                 return NotFound();
             }
             var student = await _dbContext.Students.FindAsync(id);
             if (student == null)
             {
                 return NotFound();
             }
             return student;
         }

         [HttpPost]
         public async Task<ActionResult<Student>> AddStudents(Student student)
         {
             _dbContext.Students.Add(student);
             await _dbContext.SaveChangesAsync();

             return CreatedAtAction(nameof(GetStudents), new {id=student.StudentId});
         }

         [HttpPut]
         public async Task<ActionResult> UpdateStudent(int id, Student student)
         {
             if (id != student.StudentId)
             {
                 return BadRequest();
             }
             _dbContext.Entry(student).State = EntityState.Modified;
             try
             {
                 await _dbContext.SaveChangesAsync();
             }
             catch (DbUpdateConcurrencyException)
             {

             }
         }



     }*/

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

        [HttpPost]
        public async Task<ActionResult<Student>> AddStudent(Student student)
        {
            var addedStudent = await _studentService.AddStudent(student);
            if (addedStudent != null)
            {
                return Ok(new { message = "Add Student Success", student = addedStudent });
            }
            else
            {
                return BadRequest(new { message = "Add Failed" });
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
                return NotFound(new { message = "Delete Failed" });
            }
        }

        [HttpPut("{studentId}")]
        public async Task<ActionResult> UpdateStudent(int studentId, Student student)
        {
            if (studentId != student.StudentId)
                return BadRequest(new { message = "No found Student with this ID" });

            var updated = await _studentService.UpdateStudent(student);
            if (updated)
            {
                return Ok(new { message = "Update information success" });
            }
            else
            {
                return NotFound(new { message = "Something Wrong" });
            }
        }
    }

}

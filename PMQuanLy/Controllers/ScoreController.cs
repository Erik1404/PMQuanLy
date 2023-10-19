using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PMQuanLy.Interface;
using PMQuanLy.Models;

namespace PMQuanLy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScoreController : ControllerBase
    {
        private readonly IScoreService _scoreService;

        public ScoreController(IScoreService scoreService)
        {
            _scoreService = scoreService;
        }

       /* [HttpPost("enter-scores")]
        public async Task<IActionResult> EnterScores([FromBody] Score score)
        {
            if (score == null)
            {
                return BadRequest("Invalid score data.");
            }

            try
            {
                bool result = await _scoreService.EnterScoresForCourse(
                    score.TeacherCourseId, score.CourseRegistrationId,
                    score.Score1, score.Score2, score.Score3
                );

                if (result)
                {
                    return Ok("Scores entered successfully.");
                }

                return NotFound("Course or registration not found.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }*/
    }
}


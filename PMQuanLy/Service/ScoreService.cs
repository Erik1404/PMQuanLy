using Microsoft.EntityFrameworkCore;
using PMQuanLy.Data;
using PMQuanLy.Interface;
using PMQuanLy.Models;

namespace PMQuanLy.Service
{
    public class ScoreService : IScoreService
    {
        private readonly PMQLDbContext _dbContext;

        public ScoreService(PMQLDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<bool> AddScoreForCourseRegistration(int courseRegistrationId, string score1Name, decimal score1, string score2Name, decimal score2, string score3Name, decimal score3)
        {
            try
            {
                var courseRegistration = await _dbContext.CourseRegistrations
                    .FirstOrDefaultAsync(cr => cr.CourseRegistrationId == courseRegistrationId);

                if (courseRegistration == null)
                {
                    return false; // Không tìm thấy đăng ký khóa học này
                }

                var score = new Score
                {
                    Score1_Name = score1Name,
                    Score1 = score1,
                    Score2_Name = score2Name,
                    Score2 = score2,
                    Score3_Name = score3Name,
                    Score3 = score3,
                    CourseRegistrationId = courseRegistrationId,
                };

                _dbContext.Scores.Add(score);
                await _dbContext.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<List<Score>> GetScoresForCourseRegistration(int courseRegistrationId)
        {
            return await _dbContext.Scores
                .Where(s => s.CourseRegistrationId == courseRegistrationId)
                .ToListAsync();
        }

    }
}

using Microsoft.EntityFrameworkCore;
using PMQuanLy.Data;
using PMQuanLy.Interface;
using PMQuanLy.Models;

namespace PMQuanLy.Service
{
    public class CourseEnrollmentService : ICourseEnrollmentService
    {
        private readonly PMQLDbContext _dbContext;

        public CourseEnrollmentService(PMQLDbContext dbContext)
        {
            _dbContext = dbContext;
        }

          public async Task<List<CourseEnrollment>> GetAllCourseEnrollments()
    {
        return await _dbContext.CourseEnrollments.ToListAsync();
    }

        public async Task CreateStudentScore(int courseEnrollmentId, string scoreName, double scoreValue, double scoreCoefficient)
        {
            // Kiểm tra xem CourseEnrollment có tồn tại không
            var courseEnrollment = await _dbContext.CourseEnrollments
                                .Include(ce => ce.CourseRegistration)
                                .FirstOrDefaultAsync(ce => ce.CourseEnrollmentId == courseEnrollmentId);

            string scoreClassification;
            if (scoreValue < 3)
            {
                scoreClassification = "Kém";
            }
            else if (scoreValue < 5)
            {
                scoreClassification = "Trung bình";
            }
            else if (scoreValue < 7)
            {
                scoreClassification = "Khá";
            }
            else
            {
                scoreClassification = "Giỏi";
            }
            if (courseEnrollment != null)
            {
                var studentScore = new StudentScore
                {
                    CourseEnrollmentId = courseEnrollmentId,
                    StudentId = courseEnrollment.CourseRegistration.StudentId,
                    CourseId = courseEnrollment.CourseRegistration.CourseId,
                    ScoreName = scoreName,
                    ScoreValue = scoreValue,
                    ScoreCoefficient = scoreCoefficient,
                    ScoreClassification = scoreClassification,
                };
                if (scoreValue < 0 || scoreValue > 10)
                {
                    throw new Exception("Giá trị điểm số không hợp lệ. Điểm số phải từ 0 đến 10.");
                }
                _dbContext.StudentScores.Add(studentScore);
                await _dbContext.SaveChangesAsync();
            }
            else
            {
                throw new Exception("Không tìm thấy dữ liệu bạn cần");
            }
        }
    }
}

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

            if (courseEnrollment != null)
            {
                // Kiểm tra xem ScoreName đã tồn tại cho CourseEnrollment này chưa
                var existingScore = await _dbContext.StudentScores
                    .FirstOrDefaultAsync(ss => ss.CourseEnrollmentId == courseEnrollmentId && ss.ScoreName == scoreName);

                var studentScore = new StudentScore
                {
                    CourseEnrollmentId = courseEnrollmentId,
                    StudentId = courseEnrollment.CourseRegistration.StudentId,
                    CourseId = courseEnrollment.CourseRegistration.CourseId,
                    ScoreName = scoreName,
                    ScoreValue = scoreValue,
                    ScoreCoefficient = scoreCoefficient,
                   
                };
                if (existingScore != null)
                {
                    throw new Exception("Vui lòng nhập tên cột điểm khác, cột điểm này đã tồn tại. Ví dụ: Kiểm tra 15 phút 2");
                }
                if (scoreValue < 0 || scoreValue > 10)
                {
                    throw new Exception("Giá trị điểm số không hợp lệ. Điểm số phải từ 0 đến 10.");
                }
                _dbContext.StudentScores.Add(studentScore);
                await _dbContext.SaveChangesAsync();

                // Tính trung bình cộng và lưu vào bảng Score
                CalculateAndSaveAverageScore(studentScore.StudentId, studentScore.CourseId);
            }
            else
            {
                throw new Exception("Không tìm thấy dữ liệu bạn cần");
            }
        }

        private void CalculateAndSaveAverageScore(int studentId, int courseId)
        {
            var studentScores = _dbContext.StudentScores
                .Where(ss => ss.StudentId == studentId && ss.CourseId == courseId)
                .ToList();

            if (studentScores.Count > 0)
            {
                double totalScore = studentScores.Sum(ss => ss.ScoreValue);
                double averageScore = totalScore / studentScores.Count;

                var existingScore = _dbContext.Scores
                    .FirstOrDefault(s => s.StudentScore.StudentId == studentId && s.StudentScore.CourseId == courseId);

                // Xác định ScoreClassification dựa trên điểm số
                string scoreClassification;
                if (averageScore < 3)
                {
                    scoreClassification = "Kém";
                }
                else if (averageScore < 5)
                {
                    scoreClassification = "Trung bình";
                }
                else if (averageScore < 7)
                {
                    scoreClassification = "Khá";
                }
                else
                {
                    scoreClassification = "Giỏi";
                }

                if (existingScore != null)
                {
                    existingScore.AverageScore = averageScore;
                }
                else
                {
                    var newScore = new Score
                    {
                        StudentScore = studentScores.First(),
                        StudentId = studentId,
                        CourseId = courseId,
                        AverageScore = averageScore,
                        ScoreClassification = scoreClassification,
                    };

                    _dbContext.Scores.Add(newScore);
                }

                _dbContext.SaveChanges();
            }
        }

    }
}

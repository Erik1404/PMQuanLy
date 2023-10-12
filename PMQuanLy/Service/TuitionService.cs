using Microsoft.EntityFrameworkCore;
using PMQuanLy.Data;
using PMQuanLy.Models;

namespace PMQuanLy.Service
{
    public class TuitionService : ITuitionService
    {
        private readonly PMQLDbContext _dbContext;

        public TuitionService(PMQLDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Tuition>> GetAllTuitions()
        {
            return await _dbContext.Tuitions.ToListAsync();
        }

        public async Task<Tuition> GetTuitionById(int tuitionId)
        {
            return await _dbContext.Tuitions.FindAsync(tuitionId);
        }

        public async Task<List<Tuition>> GetTuitionsByStudentId(int studentId)
        {
            return await _dbContext.Tuitions
                .Where(t => t.StudentId == studentId)
                .ToListAsync();
        }

        public async Task<Tuition> AddTuition(Tuition tuition)
        {
            _dbContext.Tuitions.Add(tuition);
            await _dbContext.SaveChangesAsync();
            return tuition;
        }

        public async Task<bool> DeleteTuition(int tuitionId)
        {
            var tuition = await _dbContext.Tuitions.FindAsync(tuitionId);
            if (tuition == null)
            {
                return false;
            }

            _dbContext.Tuitions.Remove(tuition);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateTuition(Tuition tuition)
        {
            _dbContext.Entry(tuition).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<decimal> CalculateTotalTuitionForStudent(int studentId)
        {
            // Lấy danh sách các khóa học đã đăng ký bởi học sinh
            var courseRegistrations = await _dbContext.CourseRegistrations
                .Include(cr => cr.Course)
                .Where(cr => cr.StudentId == studentId)
                .ToListAsync();

            decimal totalTuition = 0;

            foreach (var registration in courseRegistrations)
            {
                totalTuition += registration.Course.PriceCourse;
            }

            return totalTuition;
        }


    }
}

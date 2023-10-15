using Microsoft.EntityFrameworkCore;
using PMQuanLy.Data;
using PMQuanLy.Interface;
using PMQuanLy.Models;

namespace PMQuanLy.Service
{
    public class CourseYearService : ICourseYearService
    {
        private readonly PMQLDbContext _dbContext;

        public CourseYearService(PMQLDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<CourseYear>> GetAllCourseYears()
        {
            return await _dbContext.CourseYears
                .Include(cy => cy.Subjects)
                .ToListAsync();
        }

        public List<CourseYear> SearchCourseYear(string keyword)
        {

            return _dbContext.CourseYears
                .Where(s =>
                    s.CourseYearId.ToString().Contains(keyword) ||
                    s.CourseYear_Year.Contains(keyword))
                .ToList();
        }
        public async Task<CourseYear> AddCourseYear(CourseYear CourseYear)
        {
            _dbContext.CourseYears.Add(CourseYear);
            await _dbContext.SaveChangesAsync();
            return CourseYear;
        }

        public async Task<bool> DeleteCourseYear(int CourseYearId)
        {
            var CourseYear = await _dbContext.CourseYears.FindAsync(CourseYearId);
            if (CourseYear == null)
                return false;
            _dbContext.CourseYears.Remove(CourseYear);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateCourseYear(CourseYear CourseYear)
        {
            _dbContext.Entry(CourseYear).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}

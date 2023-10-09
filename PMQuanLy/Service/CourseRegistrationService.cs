using Microsoft.EntityFrameworkCore;
using PMQuanLy.Data;
using PMQuanLy.Models;

namespace PMQuanLy.Service
{
    public class CourseRegistrationService : ICourseRegistrationService
    {
        private readonly PMQLDbContext _dbContext;

        public CourseRegistrationService(PMQLDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<CourseRegistration>> GetAllCourseRegistrations()
        {
            return await _dbContext.CourseRegistrations.ToListAsync();
        }


        public async Task<CourseRegistration> AddCourseRegistration(CourseRegistration CourseRegistration)
        {
            _dbContext.CourseRegistrations.Add(CourseRegistration);
            await _dbContext.SaveChangesAsync();
            return CourseRegistration;
        }

        public async Task<bool> DeleteCourseRegistration(int CourseRegistrationId)
        {
            var CourseRegistration = await _dbContext.CourseRegistrations.FindAsync(CourseRegistrationId);
            if (CourseRegistration == null)
                return false;
            _dbContext.CourseRegistrations.Remove(CourseRegistration);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateCourseRegistration(CourseRegistration CourseRegistration)
        {
            _dbContext.Entry(CourseRegistration).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}

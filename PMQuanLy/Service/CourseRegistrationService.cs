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

        public async Task<int> CountStudentInCourse(int CourseId)
        {
            var count = _dbContext.CourseRegistrations.Where(x => x.CourseId == CourseId).Count();
            return count;
        }

        public async Task<List<Student>> ListStudentInCourse(int CourseId)
        {
            var q = from a in _dbContext.CourseRegistrations
                    join b in _dbContext.Students
                    on a.StudentId equals b.UserId
                    where a.CourseId == CourseId
                    select b;
            return await q.ToListAsync();
        }
        public async Task<Course> FindCoureByStudentId(int StudentId)
        {
            var courses = await _dbContext.CourseRegistrations.Where(x => x.StudentId == StudentId).FirstOrDefaultAsync();
            var course = await _dbContext.Courses.Where(x=> x.CourseId == courses.CourseId).FirstOrDefaultAsync();
            return course;
        }

    }
}

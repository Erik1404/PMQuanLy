using PMQuanLy.Data;

namespace PMQuanLy.Service.Teacher
{
    public class TeacherService : ITeacherService
    {
        private readonly PMQLDbContext _dbContext;

        public TeacherService(PMQLDbContext dbContext)
        {
            _dbContext = dbContext;
        }

    }
}

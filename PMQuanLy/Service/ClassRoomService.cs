using Microsoft.EntityFrameworkCore;
using PMQuanLy.Data;
using PMQuanLy.Models;

namespace PMQuanLy.Service
{
    public class ClassRoomService : IClassRoomService
    {
        private readonly PMQLDbContext _dbContext;

        public ClassRoomService(PMQLDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Classroom>> GetAllClassrooms()
        {
            return await _dbContext.Classrooms.ToListAsync();
        }

        public async Task<Classroom> GetClassroomById(int classroomId)
        {
            return await _dbContext.Classrooms.FindAsync(classroomId);
        }


        public async Task<Classroom> AddClassroom(Classroom Classroom)
        {
            _dbContext.Classrooms.Add(Classroom);
            await _dbContext.SaveChangesAsync();
            return Classroom;
        }

        public async Task<bool> DeleteClassroom(int ClassroomId)
        {
            var Classroom = await _dbContext.Classrooms.FindAsync(ClassroomId);
            if (Classroom == null)
                return false;
            _dbContext.Classrooms.Remove(Classroom);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateClassroom(Classroom Classroom)
        {
            _dbContext.Entry(Classroom).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            return true;
        }
        public List<Classroom> SearchClassrooms(string keyword)
        {

            return _dbContext.Classrooms
                .Where(s =>
                    s.ClassroomId.ToString().Contains(keyword) ||
                    s.ClassName.Contains(keyword))
                .ToList();
        }
    }
}

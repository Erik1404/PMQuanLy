using Microsoft.EntityFrameworkCore;
using PMQuanLy.Data;
using PMQuanLy.Models;

namespace PMQuanLy.Service
{
    public class SubjectService : ISubjectService
    {
        private readonly PMQLDbContext _dbContext;

        public SubjectService(PMQLDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Subject>> GetAllSubjects()
        {
            return await _dbContext.Subjects.ToListAsync();
        }
        
        public async Task<Subject> AddSubject(Subject subject)
        {
            _dbContext.Subjects.Add(subject);
            await _dbContext.SaveChangesAsync();
            return subject;
        }

        public async Task<bool> DeleteSubject(int SubjectId)
        {
            var subject = await _dbContext.Subjects.FindAsync(SubjectId);
            if (subject == null)
                return false;
            _dbContext.Subjects.Remove(subject);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateSubject(Subject subject)
        {
            _dbContext.Entry(subject).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            return true;
        }
        public List<Subject> SearchSubjects(string keyword)
        {

            return _dbContext.Subjects
                .Where(s =>
                    s.SubjectId.ToString().Contains(keyword) ||
                    s.SubjectName.Contains(keyword))
                .ToList();
        }

    }
}

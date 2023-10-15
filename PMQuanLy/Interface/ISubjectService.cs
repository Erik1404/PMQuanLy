using PMQuanLy.Models;

namespace PMQuanLy.Interface
{
    public interface ISubjectService
    {
        Task<List<Subject>> GetAllSubjects();
        List<Subject> SearchSubject(string keyword);
        Task<Subject> AddSubject(Subject subject);
        Task<bool> DeleteSubject(int SubjectId);
        Task<bool> UpdateSubject(Subject subject);

    }
}

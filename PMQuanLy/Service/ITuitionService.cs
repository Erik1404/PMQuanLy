using PMQuanLy.Models;

namespace PMQuanLy.Service
{
    public interface ITuitionService
    {
        Task<List<Tuition>> GetAllTuitions();
        Task<Tuition> GetTuitionById(int tuitionId);
        Task<List<Tuition>> GetTuitionsByStudentId(int studentId);
        Task<Tuition> AddTuition(Tuition tuition);
        Task<bool> DeleteTuition(int tuitionId);
        Task<bool> UpdateTuition(Tuition tuition);
        Task<decimal> CalculateTotalTuitionForStudent(int studentId);
    }
}

using PMQuanLy.Models;

namespace PMQuanLy.Interface
{
    public interface ITuitionService
    {
        Task<List<Tuition>> GetAllTuitions();
        
        Task<decimal> GetTuitionByStudentId(int studentId);
    }
}

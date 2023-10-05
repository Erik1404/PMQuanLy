using PMQuanLy.Models;

namespace PMQuanLy.Service
{
    public interface ITuitionService
    {
        Task<List<Tuition>> GetAllTuitions();
        Task<Tuition> AddTuition(Tuition Tuition);
        Task<bool> DeleteTuition(int TuitionId);
        Task<bool> UpdateTuition(Tuition Tuition);       
    }
}

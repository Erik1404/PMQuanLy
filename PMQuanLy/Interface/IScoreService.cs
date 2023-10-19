using PMQuanLy.Models;

namespace PMQuanLy.Interface
{
    public interface IScoreService
    {
        Task<bool> AddScoreForCourseRegistration(int courseRegistrationId, string score1Name, decimal score1, string score2Name, decimal score2, string score3Name, decimal score3);
        Task<List<Score>> GetScoresForCourseRegistration(int courseRegistrationId);
    }
}

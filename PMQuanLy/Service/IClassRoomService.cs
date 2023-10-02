using PMQuanLy.Models;

namespace PMQuanLy.Service
{
    public interface IClassRoomService
    {
        Task<List<Classroom>> GetAllClassrooms();
        List<Classroom> SearchClassrooms(string keyword);
        Task<Classroom> AddClassroom(Classroom Classroom);
        Task<bool> DeleteClassroom(int ClassroomId);
        Task<bool> UpdateClassroom(Classroom Classroom);
    }
}

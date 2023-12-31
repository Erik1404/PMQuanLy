﻿using PMQuanLy.Models;

namespace PMQuanLy.Interface
{
    public interface ITeacherService
    {

        Task<List<Teacher>> GetAllTeachers();
        /*Task<Teacher> AddTeacher(Teacher Teacher);*/
        Task<bool> DeleteTeacher(int TeacherId);
        Task<bool> UpdateTeacher(Teacher Teacher);


        Task<List<CourseEnrollment>> GetCoursesAndStudentsByTeacherId(int teacherId);

    }
}

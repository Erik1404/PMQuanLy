﻿using PMQuanLy.Models;

namespace PMQuanLy.Service
{
    public interface ICourseRegistrationService
    {
        Task<List<CourseRegistration>> GetAllCourseRegistrations();
        Task<CourseRegistration> GetCourseRegistrationById(int courseRegistrationId);
        Task<CourseRegistration> RegisterStudentForCourse(int studentId, int courseId);
        Task<bool> UnregisterStudentFromCourse(int courseRegistrationId);
        Task<List<CourseRegistration>> GetCourseRegistrationsForStudent(int studentId);
        Task<List<CourseRegistration>> GetCourseRegistrationsForCourse(int courseId);

        Task<int> CountStudentInCourse(int CourseId);
    }
}

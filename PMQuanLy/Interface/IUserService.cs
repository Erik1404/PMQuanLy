﻿using PMQuanLy.Models;

namespace PMQuanLy.Interface
{
    public interface IUserService
    {
        User Login(string email, string password);
        User Register(User newRegister);
        Student RegisterForStudent(Student newRegisterStd);
        Teacher RegisterForTeacher(Teacher newRegisterTch);
        string CheckRoleUser(string email);
    }
}

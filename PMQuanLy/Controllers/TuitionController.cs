﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PMQuanLy.Interface;
using PMQuanLy.Models;
using PMQuanLy.Service;

namespace PMQuanLy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TuitionController : ControllerBase
    {
        private readonly ITuitionService _tuitionService;
        private readonly ICourseRegistrationService _courseRegistrationService;
        private readonly ICourseService _courseService;
        private readonly IStudentService _studentService;
        private readonly IPaymentHistoryService _paymentHistoryService;

        public TuitionController(ITuitionService tuitionService,ICourseRegistrationService courseRegistrationService, IStudentService studentService, ICourseService courseService,IPaymentHistoryService paymentHistoryService)
        {
            _tuitionService = tuitionService;
            _courseRegistrationService = courseRegistrationService;
            _studentService = studentService;
            _courseService = courseService;
            _paymentHistoryService = paymentHistoryService;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllTuitions()
        {
            try
            {
                var tuitionData = await _tuitionService.GetAllTuitions();
                return Ok(tuitionData);
            }
            catch (Exception ex)
            {
                return BadRequest("Không tìm thấy dữ liệu");
            }
        }

        [HttpGet("total/{studentId}")]
        public async Task<IActionResult> GetTotalTuition(int studentId)
        {
            var student = await _studentService.GetStudentById(studentId);

            if (student == null)
            {
                return NotFound("Học sinh không tồn tại");
            }

            var totalTuition = await _tuitionService.GetTuitionByStudentId(studentId);

            // Lấy danh sách các khóa học đã đăng ký bởi học sinh
            var courseRegistrations = await _courseRegistrationService.GetCourseRegistrationsForStudent(studentId);
            var registeredCourses = new List<Course>();

            foreach (var registration in courseRegistrations)
            {
                var course = await _courseService.GetCourseById(registration.CourseId);
                if (course != null)
                {
                    registeredCourses.Add(course);
                }
            }

            var response = new
            {
                TotalTuition = totalTuition,
                RegisteredCourses = registeredCourses
            };

            return Ok(response);
        }


        [HttpPut("{tuitionId}")]
        public IActionResult UpdateTuition(int tuitionId, decimal DiscountAmount, string DiscountReason)
        {
            var updatedTuition = _tuitionService.UpdateTuition(tuitionId, DiscountAmount, DiscountReason);

            if (updatedTuition == null)
            {
                return NotFound("Không tìm thấy học phí để cập nhật.");
            }

            return Ok(updatedTuition);
        }

        [HttpPost("checkout/{tuitionId}")]
        public IActionResult Checkout(int tuitionId, decimal amountPaid)
        {
            var updatedTuition = _tuitionService.Checkout(tuitionId, amountPaid);

            if (updatedTuition != null)
            {
                // Thanh toán thành công
                return Ok("Thanh toán thành công.");
            }
            else
            {
                // Trường hợp không tìm thấy tuition hoặc lỗi khác
                return NotFound("Dữ liệu học phí không tìm thấy.");
            }
        }
    }
}

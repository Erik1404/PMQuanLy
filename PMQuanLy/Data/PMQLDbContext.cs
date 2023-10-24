using Microsoft.EntityFrameworkCore;
using PMQuanLy.Models;

namespace PMQuanLy.Data
{
    public class PMQLDbContext : DbContext
    {
        public PMQLDbContext(DbContextOptions<PMQLDbContext> options) : base(options)
        {
            
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<CourseRegistration> CourseRegistrations { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<Tuition> Tuitions { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<CourseYear> CourseYears { get; set; }
        public DbSet<StudentScore> StudentScores { get; set; }
        public DbSet<Score> Scores { get; set; }
        public DbSet<PaymentHistory> PaymentHistories { get; set; }

        public DbSet<TeacherCourse> TeacherCourses { get; set; }
        public DbSet<CourseEnrollment> CourseEnrollments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            // Liên kết giữa CourseEnrollment và CourseRegistration
            modelBuilder.Entity<CourseEnrollment>()
                .HasOne(ce => ce.CourseRegistration)
                .WithMany()
                .HasForeignKey(ce => ce.CourseRegistrationId);

            // Liên kết giữa CourseEnrollment và TeacherCourse
            modelBuilder.Entity<CourseEnrollment>()
                .HasOne(ce => ce.TeacherCourse)
                .WithMany()
                .HasForeignKey(ce => ce.TeacherCourseId)
                .OnDelete(DeleteBehavior.NoAction);

        }

    }
}

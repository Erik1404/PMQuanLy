using Microsoft.EntityFrameworkCore;
using PMQuanLy.Data;
using PMQuanLy.Interface;
using PMQuanLy.Service;
/*using PMQuanLy.Service;
*/
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//Sql server connect
builder.Services.AddDbContext<PMQLDbContext>(options => {
    options.UseSqlServer(builder.Configuration.GetConnectionString("qlhoctap"));
});
builder.Services.AddControllers();
//Sql server connect

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//add this to use Service
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<ITeacherService, TeacherService>();
builder.Services.AddScoped<ISubjectService, SubjectService>();
builder.Services.AddScoped<ICourseService, CourseService>();
builder.Services.AddScoped<ITeacherCourseService, TeacherCourseService>();
builder.Services.AddScoped<ITuitionService, TuitionService>();
builder.Services.AddScoped<ICourseRegistrationService, CourseRegistrationService>();
builder.Services.AddScoped<ICourseYearService, CourseYearService>();
builder.Services.AddScoped<IPaymentHistoryService, PaymentHistoryService>();
builder.Services.AddScoped<IScoreService, ScoreService>();


builder.Services.AddSwaggerGen();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

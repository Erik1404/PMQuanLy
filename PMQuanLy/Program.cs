using Microsoft.EntityFrameworkCore;
using PMQuanLy.Data;
using PMQuanLy.Service;

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
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IStudentService, StudentService>(); // Add this to use service

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

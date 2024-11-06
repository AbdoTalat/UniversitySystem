using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UniversityManagmentSystem.Models.Context;
using UniversityManagmentSystem.Repository.Implementaions;
using UniversityManagmentSystem.Repository.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();


//builder.Services.AddAuthentication().AddCookie(options =>
//{
//    options.Cookie.HttpOnly = true;
//    options.ExpireTimeSpan = TimeSpan.Zero;
//    options.SlidingExpiration = false;
//});

string ConnectionString = builder.Configuration.GetConnectionString("ConnString");

builder.Services.AddDbContext<UniversityContext>(optionBuilder =>
    optionBuilder.UseSqlServer(ConnectionString)
    );

builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<UniversityContext>();


builder.Services.AddScoped<IinstructorRepository, instructorRepository>();
builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<ICourseRepository, CourseRepository>();
builder.Services.AddScoped<ICourseResultRepository, CourseResultRepository>();
builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Dashboard}/{action=Welcome}/{id?}");

app.Run();

using Infrastructure.Contexts;
using Infrastructure.Models;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebApi.Configurations;

var connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Education\05._ASP.NET\Silicon_Website\Infrastructure\Data\silicon_db.mdf;Integrated Security=True;Connect Timeout=30;Encrypt=True";

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRouting(x => x.LowercaseUrls = true);
builder.Services.AddDbContext<DataContext>(x => x.UseSqlServer(connectionString));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<DataContext>()
    .AddDefaultTokenProviders();

// Add services to the container.
builder.Services.AddControllers();

// Repos & Services
builder.Services.AddScoped<AddressRepository>();
builder.Services.AddScoped<ProfilePictureRepository>();
builder.Services.AddScoped<UserProfileRepository>();
builder.Services.AddScoped<CourseRepository>();
builder.Services.AddScoped<CourseAuthorRepository>();
builder.Services.AddScoped<SavedCoursesRepository>();
builder.Services.AddScoped<ContactRequestRepository>();
builder.Services.AddScoped<CourseCategoryRepository>();

builder.Services.AddScoped<UserProfileService>();
builder.Services.AddScoped<AddressService>();
builder.Services.AddScoped<CourseService>();
builder.Services.AddScoped<ContactService>();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.RegisterJwt(builder.Configuration); // new
builder.Services.RegisterSwagger(); // new

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(x => x.SwaggerEndpoint("/swagger/v1/swagger.json", "Silicon Web Api v1"));
}

app.UseHttpsRedirection();
app.UseAuthorization(); // new
app.UseAuthentication(); // new
app.MapControllers();
app.Run();
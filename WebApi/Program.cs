using Infrastructure.Contexts;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

var connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Education\05._ASP.NET\Silicon_Website\Infrastructure\Data\silicon_db.mdf;Integrated Security=True;Connect Timeout=30;Encrypt=True";

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRouting(x => x.LowercaseUrls = true);
builder.Services.AddDbContext<DataContext>(x => x.UseSqlServer(connectionString));

// Add services to the container.
builder.Services.AddControllers();

//builder.Services.AddScoped<AddressRepository>();
//builder.Services.AddScoped<ProfilePictureRepository>();
//builder.Services.AddScoped<UserProfileRepository>();
builder.Services.AddScoped<CourseRepository>();
//builder.Services.AddScoped<CourseAuthorRepository>();
//builder.Services.AddScoped<SavedCoursesRepository>();
//builder.Services.AddScoped<UserProfileService>();
//builder.Services.AddScoped<AddressService>();
//builder.Services.AddScoped<CourseService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DataContext>(x => x.UseSqlServer(connectionString));

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

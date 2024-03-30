using Infrastructure.Contexts;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using WebApi.Configurations;

var connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Education\05._ASP.NET\Silicon_Website\Infrastructure\Data\silicon_db.mdf;Integrated Security=True;Connect Timeout=30;Encrypt=True";

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRouting(x => x.LowercaseUrls = true);
builder.Services.AddDbContext<DataContext>(x => x.UseSqlServer(connectionString));

// Add services to the container.
builder.Services.AddControllers();

// Repos
builder.Services.AddScoped<CourseRepository>();
builder.Services.AddScoped<SubscriberRepository>();
builder.Services.AddScoped<ContactRequestRepository>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.RegisterJwt(builder.Configuration); // new

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseCors(x => x.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod());

app.UseHttpsRedirection();
app.UseAuthorization(); // new
app.UseAuthentication(); // new
app.MapControllers();
app.Run();
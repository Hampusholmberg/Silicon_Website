using Infrastructure.Contexts;
using Infrastructure.Models;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Education\05._ASP.NET\Silicon_Website\Infrastructure\Data\silicon_db.mdf;Integrated Security=True;Connect Timeout=30;Encrypt=True";

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRouting(x => x.LowercaseUrls = true);

builder.Services.AddDbContext<DataContext>(x => x.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<ApplicationUser>(x =>
{
    x.User.RequireUniqueEmail = true;
    x.SignIn.RequireConfirmedEmail = false;
    x.SignIn.RequireConfirmedAccount = false;
    x.Password.RequiredLength = 8;
    x.Password.RequireUppercase = true;
}).AddEntityFrameworkStores<DataContext>();


builder.Services.AddScoped<AddressRepository>();
builder.Services.AddScoped<CourseAuthorRepository>();
builder.Services.AddScoped<CourseRepository>();
builder.Services.AddScoped<ProfilePictureRepository>();
builder.Services.AddScoped<UserProfileRepository>();
builder.Services.AddScoped<UserSavedItemRepository>();



var app = builder.Build();

app.UseHsts();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
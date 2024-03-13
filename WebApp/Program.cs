using Infrastructure.Contexts;
using Infrastructure.Models;
using Infrastructure.Repositories;
using Infrastructure.Services;
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
builder.Services.AddScoped<ProfilePictureRepository>();
builder.Services.AddScoped<UserProfileRepository>();
builder.Services.AddScoped<UserProfileService>();
builder.Services.AddScoped<AddressService>();

builder.Services.AddAuthentication().AddFacebook(x => 
{
    x.AppId = "269272056121983";
    x.AppSecret = "624813c34c7cd0e486bedfb27d6f9b29";
});

builder.Services.AddAuthentication().AddGoogle(x =>
{
    x.ClientId = "1047036215054-r8sdc3bphqdgt3aqbmp590c241t48ckd.apps.googleusercontent.com";
    x.ClientSecret = "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";
});

var app = builder.Build();

app.UseHsts();
app.UseStatusCodePagesWithReExecute("/error", "?statusCode={0}");
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
//app.UseAuthentication();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
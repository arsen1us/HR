using HR;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(builder =>
    {
        builder.LoginPath = "/Client/AuthenticationForm";
        builder.AccessDeniedPath = "/Client/AccessDenied";
    });
builder.Services.AddMvc();

var configuration_builder = new ConfigurationBuilder();
configuration_builder.SetBasePath(Directory.GetCurrentDirectory());
configuration_builder.AddJsonFile("appsettings.json");

var configuration = configuration_builder.Build();

string connectionString = configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<HrV3Context>(builder =>
{
    builder.UseSqlServer(connectionString);
});

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();
app.UseStaticFiles();

app.MapControllerRoute(
    name: "default",
    pattern: "/{controller=Client}/{action=Index}");

app.Run();



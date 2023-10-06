using la_mia_pizzeria_crud.Database;
using la_mia_pizzeria_crud.CustomLoggers;
using la_mia_pizzeria_crud.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
//var connectionString = builder.Configuration.GetConnectionString("PizzeriaContextConnection") ?? throw new InvalidOperationException("Connection string 'PizzeriaContextConnection' not found.");

builder.Services.AddDbContext<PizzeriaContext>();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<PizzeriaContext>();

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<PizzeriaContext, PizzeriaContext>();
//builder.Services.AddScoped<ICustomLoggers, CustomConsoleLogger>();
builder.Services.AddScoped<ICustomLogger, CustomFileLogger>();
var app = builder.Build();

// Configure the HTTP request pipeline. 
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();

app.UseAuthorization();


/*string pattern = "";
if (User.IsInRole("ADMIN"))
{
    pattern = "Admin/_Layout";
}
else
{
    pattern = "User/_Layout";
}*/

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=UserIndex}/{id?}");

app.MapRazorPages();

app.Run();

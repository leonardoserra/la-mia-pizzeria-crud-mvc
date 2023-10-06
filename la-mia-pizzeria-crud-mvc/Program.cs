using la_mia_pizzeria_crud.Database;
using la_mia_pizzeria_crud.CustomLoggers;
using la_mia_pizzeria_crud.Models;

var builder = WebApplication.CreateBuilder(args);

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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Pizza}/{action=Index}/{id?}");

app.Run();

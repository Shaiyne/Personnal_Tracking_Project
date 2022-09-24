using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using PersonnalTrackingProject.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(
builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddSession();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
var app = builder.Build();
var services = builder.Services;
var env = builder.Environment;
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.Use(async (context, next) =>
{
    await next();
    if (context.Response.StatusCode == 404 || context.Response.StatusCode == 400 || context.Response.StatusCode == 500 ||
    context.Response.StatusCode == 403)
    {
        context.Request.Path = "/Error/ErrorHandling";
        await next();
    }
});

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseSession();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=User}/{action=Index}/{id?}");

app.Run();

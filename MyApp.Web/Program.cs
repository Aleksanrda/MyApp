using MyApp.Web;
using Microsoft.Extensions.Logging;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.RegisterMyAppServices();
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

// Create a LoggerFactory object
var loggerFactory = LoggerFactory.Create(builder =>
{
    builder.AddFile("Logs/Log.txt");
});

builder.Services.AddSingleton(loggerFactory);

var app = builder.Build();

var logger = loggerFactory.CreateLogger("MyApp");
logger.LogInformation("MyApp application started.");

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
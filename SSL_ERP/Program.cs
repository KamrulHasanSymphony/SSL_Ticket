using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using SSL.Common.SSL.Common.Models.KendoCommon;
using SSL_ERP.Configuration.ServiceRegistration;
using SSL_ERP.Middleware;
using SSL_ERP.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
//builder.Services.Configure<IISServerOptions>(options =>
//{
//    options.AllowSynchronousIO = true;
//});
builder.Services.AddSession(options =>
{
	options.IdleTimeout = TimeSpan.FromMinutes(30); // Set your desired session timeout here
});


builder.Services.Configure<FormOptions>(x => x.MultipartBodyLengthLimit = long.MaxValue);
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("AuthContext")));

builder.Services.AddAuthServices(builder.Configuration);


builder.Services
    .AddDbServices()
    .AddAutoMapperServices();

builder.Services.AddExceptional(builder.Configuration.GetSection("Exceptional"));
builder.Services.AddSingleton<IConfiguration>(builder.Configuration);
builder.Services.AddScoped<KendoGrid<object>>();
var app = builder.Build();

app.UseSession();
app.UseExceptional();

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

app.UseMiddleware<DBMiddleware>();


app.UseEndpoints(endpoints =>
{
    endpoints.MapAreaControllerRoute(
       name: "Sample",
       areaName: "Sample",


       pattern: "Sample/{controller}/{action}");

endpoints.MapControllerRoute(
        name: "areaRoute",
        pattern: "{area:exists}/{controller}/{action}/{id?}"
    );

    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Login}/{action=Index}/{id?}");
});

app.Run();

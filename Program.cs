using Microsoft.EntityFrameworkCore;
using debtTrackingApp.Models;
using debtTrackingApp.Models.EntityModel;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;


var CorsPolicy = "CorsPolicy";

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddCors(options =>
{
  options.AddPolicy("CorsPolicy",
    policy =>
    {
      policy
      .WithOrigins("https://localhost:55508", "https://localhost:55508/admin", "https://10.110.120.3:55508").WithMethods("POST", "PUT", "DELETE", "GET").AllowAnyHeader();               
    });
});

builder.Services.AddControllersWithViews();

builder.Services.AddMvc();

builder.Services.AddDbContext<DebtTrackDBContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("constring")));

builder.Services.AddEndpointsApiExplorer();


var emailConfig = builder.Configuration
        .GetSection("EmailConfiguration")
        .Get<EmailConfiguration>();
builder.Services.AddSingleton(emailConfig);



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseCors("CorsPolicy");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");


app.MapFallbackToFile("index.html");

app.Run();

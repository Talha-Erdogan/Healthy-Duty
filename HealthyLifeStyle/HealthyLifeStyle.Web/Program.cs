using HealthyLifeStyle.Types;
using HealthyLifeStyle.Business;
using Microsoft.EntityFrameworkCore;
using HealthyLifeStyle.Business.Interfaces;
using HealthyLifeStyle.Types.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//service eklendikce buraya ekleyeceðiz
builder.Services.AddScoped<IBloodGroupService, BloodGroupService>();

var connectionString = builder.Configuration.GetConnectionString("db");
builder.Services.AddDbContext<HealthyLifeStyleDbContext>(opt => opt.UseSqlServer(connectionString));

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

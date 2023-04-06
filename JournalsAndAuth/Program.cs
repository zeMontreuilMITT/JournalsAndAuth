using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using JournalsAndAuth.Data;
using JournalsAndAuth.Areas.Identity.Data;
using JournalsAndAuth.Models;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("JournalsContextConnection") ?? throw new InvalidOperationException("Connection string 'JournalsContextConnection' not found.");

builder.Services.AddDbContext<JournalsContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<JournalsUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<JournalsContext>();


// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

using(IServiceScope scope = app.Services.CreateScope())
{
    IServiceProvider provider = scope.ServiceProvider;

    await SeedMethod.Initialize(provider);
}

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
app.UseAuthentication();;

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();

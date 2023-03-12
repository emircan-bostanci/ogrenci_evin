using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ogrencievin.Models;
using ogrencievin.Models.Entities;
using ogrencievin.Models.GeoEntity;
using ogrencievin.Models.Image;
using ogrencievin.Models.ErrorDescriber;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<Context>();
//ef core database
builder.Services.AddSingleton<ClaimManager>();
builder.Services.AddSingleton<SaveImage>(new SaveImage(Path.Combine(builder.Environment.WebRootPath,"Images")));
builder.Services.AddScoped<IRepository<Estate>,Repository<Estate>>();
builder.Services.AddScoped<IRepository<User>,Repository<User>>();


builder.Services.AddIdentity<User,IdentityRole>(_ => {
    _.Password.RequiredLength = 5; 
    _.Password.RequireNonAlphanumeric = false; 
    _.Password.RequireLowercase = true; 
    _.Password.RequireUppercase = true; 
    _.Password.RequireDigit = true;
 
}).AddErrorDescriber<CustomIdentityErrorDescriber>().AddEntityFrameworkStores<Context>();
//Authentiation
builder.Services.ConfigureApplicationCookie(options => {
    options.LoginPath = "/Sign/Login";
    options.LogoutPath = "/Sign/Logout";
});
builder.Services.AddMvc(options => {
    AuthorizationPolicy policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
    options.Filters.Add(new AuthorizeFilter(policy));
});

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
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
//TODO : SignalR implemente et
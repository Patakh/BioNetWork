using BioNetWork.Data;
using BioNetWork.Model.Account;
using BioNetWork.Model.HR.Policy;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>();

// Add services to the container.
builder.Services.AddControllersWithViews(); 
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, option =>
     {
         option.Cookie.Name = CookieAuthenticationDefaults.AuthenticationScheme; 
     });

builder.Services.AddAuthorization(option =>
{
    option.AddPolicy("Admin", policy => policy.RequireClaim("Admin"));
    option.AddPolicy("PolicyHR", policy => policy.RequireClaim("Department", "HR"));
    option.AddPolicy("ManageHR", policy =>
    policy.RequireClaim("Department", "HR")
    .RequireClaim("Manager").Requirements.Add(new HRManagerRobationRequirement(10)));
});

builder.Services.AddSingleton<IAuthorizationHandler, HRManangetProbationrequirementHandler>();

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
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.FileProviders;
using DataAccess.Data;
using Unfortunately_Project.Helpers;

var builder = WebApplication.CreateBuilder(args);

string connStr = builder.Configuration.GetConnectionString("DESKTOP-1L37OVF\\SQLEXPRESS");

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<MusicApp>(opts => opts.UseSqlServer(connStr));

//builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<MusicApp>();

//builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<MusicApp>();

builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
})
    .AddDefaultTokenProviders()
    .AddDefaultUI()
    .AddEntityFrameworkStores<MusicApp>();

builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews();

builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(10);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

using (IServiceScope scope = app.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;

    SeedExtensions.SeedRoles(serviceProvider).Wait();
    SeedExtensions.SeedAdmin(serviceProvider).Wait();
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseSession();

app.MapRazorPages();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

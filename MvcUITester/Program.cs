using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Options;
using MvcUITester.Controllers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


//builder.Services.AddHttpClient<HomeController>(opt =>
//{
//    opt.BaseAddress = new Uri("https://localhost:7299/");
//});




builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
{
    options.Cookie.Name = "MySessionCookie";
    options.LoginPath = new PathString("/Account/Login");
    options.SlidingExpiration = true;
    options.Cookie.HttpOnly = true;
    options.AccessDeniedPath = new PathString("/Account/Forbidden");
    options.ExpireTimeSpan = TimeSpan.FromMinutes(1);
    options.Cookie.MaxAge = options.ExpireTimeSpan;
    options.SlidingExpiration = true;
    options.LogoutPath = new PathString("/Account/Logout");
    options.Cookie.SameSite = SameSiteMode.Strict;
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

var cookiePolicyOptions = new CookiePolicyOptions
{
    MinimumSameSitePolicy = SameSiteMode.Strict,
    HttpOnly = Microsoft.AspNetCore.CookiePolicy.HttpOnlyPolicy.Always,
    Secure = CookieSecurePolicy.None,
};
app.UseCookiePolicy(cookiePolicyOptions);

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(opt =>
{
    opt.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
});

app.Run();

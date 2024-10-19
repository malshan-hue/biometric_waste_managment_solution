using Microsoft.AspNetCore.Authentication.Cookies;
using bwms_core_business_layer.Interfaces;
using bwms_core_business_layer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHttpContextAccessor();
builder.Services.AddControllersWithViews();
builder.Services.AddSession();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
.AddCookie(options =>
{
    options.LoginPath = "/Acccess/Login";
    options.LogoutPath = "/Acccess/Logout";
    options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
});

#region System Service

var configuration = builder.Configuration;
var connectionString = configuration.GetConnectionString("bwms");

builder.Services.AddSingleton<IDatabaseService>(provider =>
{
    var dbService = new DatabaseServiceImpl();
    dbService.SetConnectionString(connectionString);
    return dbService;
});
#endregion

#region USER SERVICE
builder.Services.AddSingleton<IUserService, UserServiceImpl>();
#endregion
var app = builder.Build();

Stripe.StripeConfiguration.ApiKey = "sk_test_51PB1CBIkQxNhsnF8eZtM0ZCGLxLQvILsgHjC6PumS3hntAXOy9w5TD9Q4MfBNB0cDrG3LXAexqaPvFNVdBevDt4c00ewD6mnHv";

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
app.UseSession();

app.MapAreaControllerRoute(
    name: "Authority",
    areaName: "Authority",
    pattern: "Authority/{controller=Dashboard}/{action=Index}/{id?}");

app.MapAreaControllerRoute(
    name: "Residents",
    areaName: "Residents",
    pattern: "Residents/{controller=Dashboard}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "areaRoute",
    pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Access}/{action=Login}/{id?}");

app.Run();

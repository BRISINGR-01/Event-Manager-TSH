using Infrastructure;
using Logic.Configuration;
using Logic.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using Web;
using Web.Middlewares;
using Web.Middlewares.Authentication;
using Web.Middlewares.Errors;

var builder = WebApplication.CreateBuilder(args);
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddErrorLogger();

// Add services to the container.
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
{
    options.LoginPath = new PathString("/Pages/Authentication/LogIn");
});

builder.Services.Configure<HashingConfig>(builder.Configuration.GetSection(HashingConfig.Section));
builder.Services.Configure<VapidConfig>(builder.Configuration.GetSection(VapidConfig.Section));
builder.Services.Configure<EmailConfig>(builder.Configuration.GetSection(EmailConfig.Section));

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();

builder.Services.AddRazorPages();

builder.Services.AddSingleton<IManager, Manager>();
builder.Services.AddScoped<IAuthenticationContext, AuthenticationContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.

if (!app.Environment.IsDevelopment())
{
    app.UseStatusCodePagesWithReExecute("/Errors/404");
    app.UseExceptionHandler("/Errors/404");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    //app.UseHsts();
}

app.UseMiddleware<ErrorLoggingMiddleware>();
app.UseMiddleware<IpHandlerMiddleware>();
app.UseMiddleware<IdentityUserMiddleware>();

app.MapPost("subscribe-to-notifications", Utilities.SubscribeTonotifications);

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();

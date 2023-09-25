using Microsoft.EntityFrameworkCore.Infrastructure;
using SEP_Web.Database;
using SEP_Web.Auth;
using SEP_Web.Models;
using SEP_Web.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<CookiePolicyOptions>(options =>
{
    options.CheckConsentNeeded = context => false;
    options.MinimumSameSitePolicy = SameSiteMode.None;
});

builder.Services.AddMemoryCache();
builder.Services.AddSession();

// Add framework services.
builder.Services.AddMvc();

builder.Services.AddDbContext<SEP_WebContext>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<UserAdministrator>();
builder.Services.AddScoped<IUserAdministratorServices, UserAdministratorServices>();
builder.Services.AddScoped<IInstituitionServices, InstituitionServices>();
builder.Services.AddScoped<IUserSession, Session>();

builder.Services.AddMvc();

builder.Services.AddSession(x => // cookies use configuration;
{
    x.Cookie.HttpOnly = true;
    x.Cookie.IsEssential = true;
});

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

var logFac = app.Services.GetService<ILoggerFactory>(); // call to the ILoggerFactory service, enabling.

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

//Implementando recursos do uso de sessão ao Pipeline da aplicação
app.UseHttpsRedirection();
app.UseCookiePolicy();
app.UseSession();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseSession(); // enabling sessions

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Index}/{id?}");

logFac.AddFile("Logs/log-{Date}.txt", LogLevel.Error);

app.Run();

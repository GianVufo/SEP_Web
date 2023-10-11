using SEP_Web.Helper.Authentication;
using SEP_Web.Helper.Validations;
using SEP_Web.Database;
using SEP_Web.Services;
using SEP_Web.Models;
using Serilog.Events;
using Serilog;

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
builder.Services.AddScoped<IUserEvaluatorServices, UserEvaluatorServices>();
builder.Services.AddScoped<IValidationUsers, ValidationUsers>();
builder.Services.AddScoped<IInstituitionServices, InstituitionServices>();
builder.Services.AddScoped<IDivisionServices, DivisionServices>();
builder.Services.AddScoped<ISectionServices, SectionServices>();
builder.Services.AddScoped<ISectorServices, SectorServices>();
builder.Services.AddScoped<IUserSession, UserSession>();

builder.Services.AddMvc();

builder.Services.AddSession(x => // cookies use configuration;
{
    x.Cookie.HttpOnly = true;
    x.Cookie.IsEssential = true;
});

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

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


Log.Logger = new LoggerConfiguration()
    .WriteTo.Logger(lc => lc
        .WriteTo.File("Logs/log-{Date}.txt", rollingInterval: RollingInterval.Day)
        .WriteTo.Logger(lc2 => lc2
            .Filter.ByIncludingOnly(e => e.Level == LogEventLevel.Error)
            .WriteTo.File("Logs/Errors/log-error-{Date}.txt", rollingInterval: RollingInterval.Month)
        )
        .WriteTo.Logger(lc3 => lc3
            .Filter.ByIncludingOnly(e => e.Level == LogEventLevel.Warning)
            .WriteTo.File("Logs/Warning/log-warning-{Date}.txt", rollingInterval: RollingInterval.Day)
        )
    ).CreateLogger();

var logFac = app.Services.GetRequiredService<ILoggerFactory>();
logFac.AddSerilog();

app.Run();

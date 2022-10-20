using Microsoft.EntityFrameworkCore;
using TrialBalanceWebApp.Data;
using TrialBalanceWebApp.Services.DataServices;
using TrialBalanceWebApp.Services.Logging.Configuration;

var builder = WebApplication.CreateBuilder(args);
builder.Host.ConfigureLogging(logging =>
{
    logging.ClearProviders();
    logging.AddConsole();
});

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.UseNpgsql(builder.Configuration.GetConnectionString("DataAccessPostgreSqlProvider")));

builder.ConfigureSerilog();
builder.Services.RegisterLoggingInterfaces();
builder.Services.AddRepositories();
builder.Services.AddDataServices();


builder.Services.AddScoped<IDbInitializer, DbInitiailizer>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

using (var serviceScope = app.Services.CreateScope())
{
    var services = serviceScope.ServiceProvider;
    var myDependency = services.GetRequiredService<IDbInitializer>();
    myDependency.Seed().Wait();
}

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

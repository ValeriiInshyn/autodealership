using CourseWork;
using Radzen;
using CourseWork.Components;
using CourseWork.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddRazorComponents().AddInteractiveServerComponents().AddHubOptions(options => options.MaximumReceiveMessageSize = 10 * 1024 * 1024);
builder.Services.AddControllers();
builder.Services.AddRadzenComponents();
builder.Services.AddHttpClient();
builder.Services.AddTransient<AutoDealershipService>();
builder.Services.AddDbContext<CourseWork.Data.AutoDealershipContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("AutoDealershipConnection"));
});
builder.Services.AddScoped<AutoDealershipOLAPService>();
builder.Services.AddDbContext<CourseWork.Data.AutoDealershipOLAPContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("AutoDealershipOLAPConnection"));
});
builder.Services.AddScoped<CourseWork.AutoDealershipOLAPService>();
var app = builder.Build();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.MapControllers();
app.UseStaticFiles();
app.UseAntiforgery();
app.MapRazorComponents<App>().AddInteractiveServerRenderMode();
app.Run();
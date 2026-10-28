using AntiHD.web.Components;
using AntiHD.Shared.Services;
using AntiHD.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;


var builder = WebApplication.CreateBuilder(args);

var dbPath = Path.GetFullPath(
    Path.Combine(builder.Environment.ContentRootPath, "..", "AntiHD.Shared", "AntiHD.db"));

builder.Services.AddDbContext<AntiHD_DbContext>(options =>
    options.UseSqlite($"Data Source={dbPath}"));

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents(options => options.DetailedErrors = true);

builder.Services.AddScoped<IUserServices, UserServices>();
builder.Services.AddScoped<IPasswordHasher<Users>, PasswordHasher<Users>>();
builder.Services.AddScoped<IDtoMapper, DtoMapper>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddAdditionalAssemblies(typeof(AntiHD.UI.Pages.UiHome).Assembly);
app.Run();

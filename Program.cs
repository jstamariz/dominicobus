using DominicoBus.Services;
using DominicoBus.Db;
using Microsoft.EntityFrameworkCore;
using DominicoBus.Components;
using Microsoft.AspNetCore.Components.Endpoints;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddControllers();

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddIdentityCore<IdentityUser>()
    .AddEntityFrameworkStores<AppDbContext>();

// TODO: Extract service initialization to it's own class
builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("Default")));
builder.Services.AddSingleton<ContentService>();
builder.Services.AddScoped<UserService>();

builder.Services.AddAuthentication();
builder.Services.AddAuthorization();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseAuthentication();
app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapRazorPages();
app.MapControllers();

app.MapRazorComponents<DominicoBus.Components.Forms.UserForm>();

app.MapGet("/", () => Results.Redirect("Home"));

app.Run();
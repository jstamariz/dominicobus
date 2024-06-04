using DominicoBus.Services;
using DominicoBus.Db;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// TODO: Extract service initialization to it's own class
builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("Default")));

builder.Services.AddSingleton<ContentService>();

builder.Services.AddAuthentication();
builder.Services.AddAuthorization();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseStaticFiles();

app.MapRazorPages();
app.MapControllers();

app.MapGet("/", () => Results.Redirect("Home"));

app.Run();
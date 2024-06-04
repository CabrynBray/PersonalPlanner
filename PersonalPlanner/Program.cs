using PersonalPlanner.Components;
using PersonalPlanner.Data;
using PersonalPlanner.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using DotNetEnv;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

// Load environment variables from .env file
Env.Load();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.Cookie.Name = "auth_token";
        options.LoginPath = "/login";
        options.Cookie.MaxAge = TimeSpan.FromMinutes(30);
        options.AccessDeniedPath = "/access-denied";
    });

builder.Services.AddCascadingAuthenticationState();

// Configure MongoDB settings with environment variables
var mongoDBSettings = new MongoDBSettings
{
    ConnectionString = Environment.GetEnvironmentVariable("MONGODB_CONNECTION_STRING"),
    DatabaseName = Environment.GetEnvironmentVariable("MONGODB_DATABASE_NAME")
};
builder.Services.AddSingleton(mongoDBSettings);
builder.Services.AddSingleton<MongoDBContext>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

// Create a scope to resolve services
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var logger = services.GetRequiredService<ILogger<Program>>();

    try
    {
        var context = services.GetRequiredService<MongoDBContext>();
        // Attempt to connect to the database to verify connection
        var filter = Builders<User>.Filter.Empty; // Create an empty filter to match all documents
        var user = context.Users.Find(filter).FirstOrDefault(); // Use the correct Find method
        if (user != null)
        {
            logger.LogInformation("Successfully connected to MongoDB.");
        }
        else
        {
            logger.LogInformation("Successfully connected to MongoDB, but no users were found.");
        }
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "An error occurred while connecting to MongoDB.");
    }
}

app.Run();

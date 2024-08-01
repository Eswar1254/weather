using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using MongoDB.Driver;
using weather.Services; // Ensure this namespace is included
using MudBlazor.Services;
using Blazored.LocalStorage; // Add this to use Blazored.LocalStorage
using weather.Components.Account;
using weather.Components;
using weather.Data;

namespace weather
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();

            // Configure HttpClient
            builder.Services.AddHttpClient();

            // Configure MongoDB connection
            var mongoConnectionString = builder.Configuration.GetConnectionString("MongoDbConnection")
                ?? throw new InvalidOperationException("MongoDB connection string 'MongoDbConnection' not found.");

            var mongoClient = new MongoClient(mongoConnectionString);
            var mongoDatabase = mongoClient.GetDatabase("TestDatabase");

            builder.Services.AddSingleton<IMongoDatabase>(mongoDatabase);
            builder.Services.AddScoped<IUserStore<ApplicationUser>, MongoUserStore>();

            // Register Blazored.LocalStorage services
            builder.Services.AddBlazoredLocalStorage(); // Required for FavoriteCityService

            // Register FavoriteCityService
            builder.Services.AddScoped<FavoriteCityService>();

            // Configure Identity with MongoDB
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.SignIn.RequireConfirmedAccount = true;
            })
            .AddUserStore<MongoUserStore>() // Register the custom MongoUserStore
            .AddRoleStore<MongoRoleStore<IdentityRole>>() // Register the custom MongoRoleStore
            .AddDefaultTokenProviders();

            builder.Services.AddSingleton<IEmailSender<ApplicationUser>, IdentityNoOpEmailSender>();

            // Add MudBlazor services
            builder.Services.AddMudServices();
            builder.Services.AddSingleton<IUserStore<ApplicationUser>>(serviceProvider =>
            {
                var database = serviceProvider.GetRequiredService<IMongoDatabase>();
                return new MongoUserStore(database);
            });

            // Configure authentication
            // Note: Default authentication configuration is already handled by AddIdentity
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = IdentityConstants.ApplicationScheme;
                options.DefaultChallengeScheme = IdentityConstants.ApplicationScheme;
            });

            // Register IdentityRedirectManager
            builder.Services.AddScoped<IdentityRedirectManager>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseAntiforgery();

            // Authentication and authorization middleware
            app.UseAuthentication();
            app.UseAuthorization();

            // Custom middleware for redirecting unauthenticated users
            app.Use(async (context, next) =>
            {
                // Allow access to specific paths without authentication
                if (!context.User.Identity.IsAuthenticated &&
                    !context.Request.Path.StartsWithSegments("/Account") &&
                    !context.Request.Path.StartsWithSegments("/FavoriteCity") &&
                    !context.Request.Path.StartsWithSegments("/weather"))
                {
                    context.Response.Redirect("/Account/Login");
                }
                else
                {
                    await next();
                }
            });

            // Custom middleware for handling logout
            app.Use(async (context, next) =>
            {
                if (context.Request.Path.StartsWithSegments("/Account/Logout"))
                {
                    await context.SignOutAsync(IdentityConstants.ApplicationScheme);
                    context.Response.Redirect("/Account/Login");
                }
                else
                {
                    await next();
                }
            });

            // Map Blazor components
            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();

            // Map additional endpoints if needed
            app.MapAdditionalIdentityEndpoints();

            app.Run();
        }
    }
}
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MudBlazor.Services;
using weather.Components;
using weather.Components.Account;
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

            // Authentication and Identity
            builder.Services.AddCascadingAuthenticationState();
            builder.Services.AddScoped<IdentityUserAccessor>();
            builder.Services.AddScoped<IdentityRedirectManager>();
            builder.Services.AddScoped<AuthenticationStateProvider, IdentityRevalidatingAuthenticationStateProvider>();

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultScheme = IdentityConstants.ApplicationScheme;
                options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
            })
            .AddIdentityCookies();

            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddIdentityCore<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddSignInManager()
                .AddDefaultTokenProviders();

            builder.Services.AddSingleton<IEmailSender<ApplicationUser>, IdentityNoOpEmailSender>();

            // Add MudBlazor services
            builder.Services.AddMudServices();

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

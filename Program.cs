using Application.Context;
using Application.Services;
using Application.Services.Categorys;
using Application.Services.Products;
using Microsoft.EntityFrameworkCore;

namespace Application;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        ConfigureServices(builder);
        var app = builder.Build();

        ConfigureMiddleware(app);

        app.Run();
    }

    /// <summary>
    /// Configure services and dependency injection.
    /// </summary>
    private static void ConfigureServices(WebApplicationBuilder builder)
    {
        // Add services to the container.
        builder.Services.AddControllersWithViews();

        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

        builder.Services.AddScoped<ICategorieService, CategoryService>();
        builder.Services.AddScoped<IProductService, ProductService>();

        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowAll", policyBuilder =>
            {
                policyBuilder.AllowAnyOrigin()
                             .AllowAnyMethod()
                             .AllowAnyHeader();
            });
        });
    }

    /// <summary>
    /// Configure middleware for the application.
    /// </summary>
    private static void ConfigureMiddleware(WebApplication app)
    {
        // Error handling and HSTS configuration.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts(); // Default HSTS is 30 days.
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseRouting();
        app.UseCors("AllowAll");
        app.UseAuthorization();

        // Define routes.
        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");
        app.MapControllers();
    }
}

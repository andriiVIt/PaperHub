using System.Text.Json;
using DataAccess;
using API.Middleware;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Service;
namespace API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        
        // Set the URL where the application will run
        builder.WebHost.UseUrls("http://localhost:5000");

        // Configure AppOptions from appsettings.json
        builder.Services.Configure<AppOptions>(builder.Configuration.GetSection("AppOptions"));

        // Configure DbContext with PostgreSQL
        builder.Services.AddDbContext<MyDbContext>((serviceProvider, options) =>
        {
            var appOptions = serviceProvider.GetRequiredService<IOptions<AppOptions>>().Value;
            Console.WriteLine(appOptions.DbConnectionString);
            options.UseNpgsql(appOptions.DbConnectionString);
        });

        // Register services in Dependency Injection (DI) container
        builder.Services.AddScoped<IPaperService, PaperService>();
        builder.Services.AddScoped<ICustomerService, CustomerService>();
        builder.Services.AddScoped<IOrderService, OrderService>();
        builder.Services.AddScoped<IOrderEntryService, OrderEntryService>();
        builder.Services.AddScoped<IPropertyService, PropertyService>();
        builder.Services.AddScoped<IPaperPropertyService, PaperPropertyService>();

        // Register repositories in DI container
        builder.Services.AddScoped<IPaperRepo, PaperRepo>();
        builder.Services.AddScoped<ICustomerRepo, CustomerRepo>();
        builder.Services.AddScoped<IOrderRepo, OrderRepo>();
        builder.Services.AddScoped<IOrderEntryRepo, OrderEntryRepo>();
        builder.Services.AddScoped<IPropertyRepo, PropertyRepo>();
        builder.Services.AddScoped<IPaperPropertyRepo, PaperPropertyRepo>();

        // Add controllers and Swagger for API documentation
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddOpenApiDocument();

        var app = builder.Build();

        // Output options to the console for debugging
        var options = app.Services.GetRequiredService<IOptions<AppOptions>>().Value;
        Console.WriteLine("APP OPTIONS: " + JsonSerializer.Serialize(options));

        // Configure Swagger for development environment
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.RoutePrefix = string.Empty;  // Set the root path for Swagger
            });
        }

        // Configure additional middleware and services
        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseOpenApi();  // Enable OpenAPI documentation
        app.UseSwaggerUi();  // Enable Swagger UI
        app.UseStatusCodePages();
        app.UseCors(config => config.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());  // Set up CORS policy
        app.UseMiddleware<RequestResponseLoggingMiddleware>();  // Custom middleware for logging requests/responses

        // Map controllers for routing
        app.MapControllers();

        // Start the application
        app.Run();
    }
}

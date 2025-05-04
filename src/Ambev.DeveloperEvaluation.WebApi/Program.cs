using Ambev.DeveloperEvaluation.Application;
using Ambev.DeveloperEvaluation.Common.HealthChecks;
using Ambev.DeveloperEvaluation.IoC;
using Ambev.DeveloperEvaluation.IoC.Logging;
using Ambev.DeveloperEvaluation.WebApi.Middleware;
using Serilog;

namespace Ambev.DeveloperEvaluation.WebApi;

public class Program
{
    public static void Main(string[] args)
    {
        try
        {
            Log.Information("Starting web application");

            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
            builder.AddDefaultLogging();

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();

            builder.AddBasicHealthChecks();
            builder.Services.AddSwaggerGen(options =>
            {
                options.EnableAnnotations();
            });

            builder.RegisterDependencies();

            builder.RegisterAssemblyDependencies(
                    typeof(ApplicationLayer).Assembly,
                    typeof(Program).Assembly
            );

            var app = builder.Build();
            app.UseMiddleware<ValidationExceptionMiddleware>();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseBasicHealthChecks();

            app.MapControllers();

            app.Run();
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "Application terminated unexpectedly");
            System.Console.WriteLine(ex.ToString());
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }
}

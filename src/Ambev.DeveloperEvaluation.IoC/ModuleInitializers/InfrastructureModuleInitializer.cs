using Ambev.DeveloperEvaluation.Common.Abstractions.Security;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.ORM;
using Ambev.DeveloperEvaluation.ORM.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Ambev.DeveloperEvaluation.IoC.ModuleInitializers;

public class InfrastructureModuleInitializer : IModuleInitializer
{
    public void Initialize(WebApplicationBuilder builder)
    {
        if (!builder.Environment.IsEnvironment("Test"))
            builder.Services.AddDbContext<DefaultContext>(options =>
                    options.UseNpgsql(
                        builder.Configuration.GetConnectionString("DefaultConnection"),
                        b => b.MigrationsAssembly("Ambev.DeveloperEvaluation.ORM")
                    )
                );

        builder.Services.AddScoped<DbContext>(provider => provider.GetRequiredService<DefaultContext>());
        builder.Services.AddScoped<IUserRepository, UserRepository>();
        builder.Services.AddScoped<IProductRepository, ProductRepository>();
        builder.Services.AddScoped<ISaleRepository, SaleRepository>();
        builder.Services.AddScoped<ISaleItemRepository, SaleItemRepository>();
    }

    public static async Task ApplyMigrate(WebApplication app)
    {
        if (app.Environment.IsEnvironment("Test"))
            return;

        using (var scope = app.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            var context = services.GetRequiredService<DefaultContext>();
            context.Database.Migrate();

            // Executa o seed
            var passwordHasher = services.GetRequiredService<IPasswordHasher>();

            var existingUser = await context.Users.AnyAsync(x => x.Role == Domain.Enums.UserRole.Admin);

            if (existingUser)
                return;

            var user = new UserEntity
            {
                Id = Guid.NewGuid(),
                Username = "developer.evaluation",
                Email = "developer.evaluation@test.com",
                Phone = "+5569537719157",
                Password = passwordHasher.HashPassword("Test@1234"),
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                Status = Domain.Enums.UserStatus.Active,
                Role = Domain.Enums.UserRole.Admin
            };

            await context.Users.AddAsync(user);
            await context.SaveChangesAsync();
        }
    }
}
using DocuCheck.Application.Providers;
using DocuCheck.Application.Repositories.Interfaces;
using DocuCheck.Application.Services;
using DocuCheck.Infrastructure.Clients;
using DocuCheck.Infrastructure.Extensions;
using DocuCheck.Infrastructure.Persistence;
using DocuCheck.Infrastructure.Persistence.Repositories;
using DocuCheck.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace DocuCheck.Infrastructure;

public static class DependencyInjection
{
    public static void AddInfrastructure(this IServiceCollection services, IConfigurationManager configuration)
    {
        services.AddScoped<IMinistryOfInteriorService, MinistryOfInteriorService>();
        services.AddPersistence(configuration);
        services.AddRepositories();
        services.ConfigureHttpClients(configuration);
    }
    
    private static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<ICheckHistoryRepository, CheckHistoryRepository>();
    }
    
    private static void ConfigureHttpClients(this IServiceCollection services, IConfigurationManager configuration)
    {
        services.AddHttpClient<MinistryOfInteriorClient>(client =>
        {
            client.BaseAddress = new Uri(configuration.GetMinistryApiBaseAddress());
        });
    }
    
    private static void AddPersistence(this IServiceCollection services, IConfigurationManager configuration)
    {
        var connectionString = configuration
            .GetConnectionString(DocuCheckDbContext.ConnectionStringKey);
        services.AddDbContext<DocuCheckDbContext>(options =>
            options.UseSqlite(connectionString));

        using var scope = services.BuildServiceProvider().CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<DocuCheckDbContext>();
        var envProvider = scope.ServiceProvider.GetRequiredService<IEnvironmentProvider>();
        
        if (envProvider.EnvironmentName == "Development")
        {
            if (dbContext.Database.GetPendingMigrations().Any())
            {
                Log.Information("Applying migrations...");
                dbContext.Database.Migrate();
                Log.Information("Migrations applied");
            }
        }
    }
}
using DocuCheck.Application;
using DocuCheck.Application.Services.Interfaces;
using DocuCheck.Infrastructure;
using DocuCheck.Main.Providers;

namespace DocuCheck.Main.Extensions
{
    internal static class WebApplicationBuilderExtensions
    {
        internal static void ConfigureServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IEnvironmentProvider>(services =>
            {
                var env = services.GetRequiredService<IWebHostEnvironment>();
                return new HostEnvironmentProvider(env.EnvironmentName);
            });
            builder.Services.AddApplication();
            builder.Services.AddInfrastructure(builder.Configuration);
            
            builder.Services.ConfigureCors();
        }
        
        private static void ConfigureCors(this IServiceCollection services) =>
            services.AddCors(options =>
            {
                options.AddPolicy("SsePolicy", policy =>
                {
                    policy.WithOrigins("http://localhost:5173", "https://localhost:5173")
                        .WithMethods("GET", "POST")
                        .WithHeaders("Content-Type")
                        .AllowCredentials()
                        .SetIsOriginAllowedToAllowWildcardSubdomains()
                        .WithExposedHeaders("Content-Type");
                });
            });
    }
}
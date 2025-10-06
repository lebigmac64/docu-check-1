using DocuCheck.Application.Handlers;
using DocuCheck.Application.Handlers.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace DocuCheck.Application;

public static class DependencyInjection
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddHandlers();
    }
    
    public static void AddHandlers(this IServiceCollection services)
    {
        services.AddScoped<IDocumentHandler, DocumentHandler>();
    }
}
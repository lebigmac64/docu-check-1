using DocuCheck.Application.Services;
using DocuCheck.Application.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace DocuCheck.Application;

public static class DependencyInjection
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IDocumentService, DocumentService>();
    }
}
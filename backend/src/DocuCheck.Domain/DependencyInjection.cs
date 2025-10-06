using DocuCheck.Domain.Services;
using DocuCheck.Domain.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace DocuCheck.Domain;

public static class DependencyInjection
{
    public static void AddDomain(this IServiceCollection services)
    {
        services.AddScoped<IDocumentService, DocumentService>();
    }
}
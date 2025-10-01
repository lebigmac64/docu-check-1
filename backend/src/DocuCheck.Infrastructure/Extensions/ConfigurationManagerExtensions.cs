using Microsoft.Extensions.Configuration;

namespace DocuCheck.Infrastructure.Extensions;

public static class ConfigurationManagerExtensions
{
    public static string GetMinistryApiBaseAddress(this IConfigurationManager configuration)
    {
        var baseAddress = configuration["HttpClients:MinistryApi:BaseAddress"];
        if (string.IsNullOrWhiteSpace(baseAddress))
        {
            throw new ArgumentNullException(nameof(baseAddress), "MinistryApi BaseAddress is not configured.");
        }

        return baseAddress;
    }
        
}
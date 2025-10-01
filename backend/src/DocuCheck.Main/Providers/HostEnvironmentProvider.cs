using DocuCheck.Application.Services.Interfaces;

namespace DocuCheck.Main.Providers;

public class HostEnvironmentProvider(string environmentName) : IEnvironmentProvider
{
    public string EnvironmentName { get; init; } = environmentName;
}
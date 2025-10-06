using DocuCheck.Application.Providers;

namespace DocuCheck.Main.Providers;

public class HostEnvironmentProvider(string environmentName) : IEnvironmentProvider
{
    public string EnvironmentName { get; init; } = environmentName;
}
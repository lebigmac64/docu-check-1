namespace DocuCheck.Application.Providers;

public interface IEnvironmentProvider
{
    string EnvironmentName { get; init; }
}
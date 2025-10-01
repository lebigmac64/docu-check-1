namespace DocuCheck.Application.Services.Interfaces;

public interface IEnvironmentProvider
{
    string EnvironmentName { get; init; }
}
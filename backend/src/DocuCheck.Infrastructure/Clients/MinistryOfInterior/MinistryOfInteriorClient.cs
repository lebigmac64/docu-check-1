using DocuCheck.Application.Services.Interfaces;
using DocuCheck.Domain.Entities.ChecksHistory.Enums;
using DocuCheck.Domain.Entities.ChecksHistory.ValueObjects;
using DocuCheck.Infrastructure.Clients.Responses;
using DocuCheck.Infrastructure.Helpers;

namespace DocuCheck.Infrastructure.Clients.MinistryOfInterior;

internal class MinistryOfInteriorClient(
    HttpClient client, 
    IEnvironmentProvider environmentProvider) 
    : IMinistryOfInteriorClient
{
    public async Task<CheckResult> CheckValidityAsync(DocumentNumber documentNumber, DocumentType documentType)
    {
        var requestUri = $"neplatne-doklady/doklady.aspx?dotaz={documentNumber.Value}&doklad={(byte)documentType}";
        using var response = await client.GetAsync(requestUri);

        if (environmentProvider.EnvironmentName != "Testing")
        {
            var rndDelay = new Random().Next(3000, 10000);
            await Task.Delay(rndDelay);
        }

        var stringContent = await response.Content.ReadAsStringAsync();
        var checkValidityResponse = stringContent.FromXml<CheckDocumentValidityDto>();

        if (checkValidityResponse == null)
        {
            return CheckResult.Error(documentType, "Failed to parse response from Ministry of Interior API");
        }
        
        var parseResult = checkValidityResponse.ParseCheckResult(documentType);
        
        return parseResult;
    }
}
using DocuCheck.Application.Providers;
using DocuCheck.Application.Services;
using DocuCheck.Domain.Entities.ChecksHistory.Enums;
using DocuCheck.Domain.Entities.ChecksHistory.ValueObjects;
using DocuCheck.Infrastructure.Clients;
using DocuCheck.Infrastructure.Helpers;
using DocuCheck.Infrastructure.Responses;
using Microsoft.Extensions.Logging;

namespace DocuCheck.Infrastructure.Services;

internal class MinistryOfInteriorService(
    MinistryOfInteriorClient client, 
    IEnvironmentProvider environmentProvider,
    ILogger<MinistryOfInteriorService> logger)
    : IMinistryOfInteriorService
{
    public async Task<CheckResult> CheckDocumentValidityAsync(DocumentNumber documentNumber, DocumentType docType)
    {
        try
        {
            var checkValidityResponse = await client.GetDocumentValidityStateAsync(
                documentNumber,
                docType);
            
            if (environmentProvider.EnvironmentName != "Testing")
            {
                var rndDelay = new Random().Next(3000, 10000);
                await Task.Delay(rndDelay);
            }

            return checkValidityResponse.ParseCheckResult(docType);
        }
        catch (HttpRequestException ex)
        {
            logger.LogError(ex, "HTTP error occurred while checking document validity for {DocumentNumber} of type {DocumentType}",
                documentNumber.Value, docType);
            return CheckResult.Error(docType, "An unexpected error occurred while checking document validity");
        }
    }
}

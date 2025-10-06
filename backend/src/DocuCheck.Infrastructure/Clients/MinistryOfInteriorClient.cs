using DocuCheck.Domain.Entities.ChecksHistory.Enums;
using DocuCheck.Domain.Entities.ChecksHistory.ValueObjects;
using DocuCheck.Infrastructure.Helpers;
using DocuCheck.Infrastructure.Responses;

namespace DocuCheck.Infrastructure.Clients;

internal class MinistryOfInteriorClient(HttpClient client) 
{
    internal async Task<CheckDocumentValidityDto> GetDocumentValidityStateAsync(DocumentNumber documentNumber,
        DocumentType documentType)
    {
        var requestUri = $"neplatne-doklady/doklady.aspx?dotaz={documentNumber.Value}&doklad={(byte)documentType}";

        var response = await client.GetAsync(requestUri);
        
        var stringContent = await response.Content.ReadAsStringAsync();
            
        var data = stringContent.FromXml<CheckDocumentValidityDto>() 
            ?? throw new InvalidOperationException("Failed to parse response from Ministry of Interior");

        return data.IsValidResponse
            ? data
            : throw new HttpRequestException("Received invalid response from Ministry of Interior");
    }
}
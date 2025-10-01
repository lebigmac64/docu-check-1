using DocuCheck.Application.Services.Interfaces;
using DocuCheck.Domain.Entities.ChecksHistory.Enums;
using DocuCheck.Domain.Entities.ChecksHistory.ValueObjects;
using DocuCheck.Infrastructure.Clients.MinistryOfInterior;

namespace DocuCheck.Infrastructure.Services;

internal class MinistryOfInteriorService(MinistryOfInteriorClient ministryOfInteriorClient) 
    : IMinistryOfInteriorService
{
    public async Task<CheckResult> CheckDocumentValidityAsync(DocumentNumber docNumber, DocumentType documentType)
    {
        return await ministryOfInteriorClient.CheckValidityAsync(docNumber.Value, documentType);
    }
}
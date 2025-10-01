using DocuCheck.Domain.Entities.ChecksHistory.Enums;
using DocuCheck.Domain.Entities.ChecksHistory.ValueObjects;

namespace DocuCheck.Application.Services.Interfaces;

public interface IMinistryOfInteriorService
{
    Task<CheckResult> CheckDocumentValidityAsync(DocumentNumber docNumber, DocumentType documentType);
}
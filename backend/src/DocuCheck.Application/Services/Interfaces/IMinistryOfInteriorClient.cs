using DocuCheck.Domain.Entities.ChecksHistory.Enums;
using DocuCheck.Domain.Entities.ChecksHistory.ValueObjects;

namespace DocuCheck.Application.Services.Interfaces;

public interface IMinistryOfInteriorClient
{
    Task<CheckResult> CheckValidityAsync(DocumentNumber documentNumber, DocumentType documentType);
}
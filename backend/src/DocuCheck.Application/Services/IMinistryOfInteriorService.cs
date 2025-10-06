using DocuCheck.Domain.Entities.ChecksHistory.Enums;
using DocuCheck.Domain.Entities.ChecksHistory.ValueObjects;

namespace DocuCheck.Application.Services;

public interface IMinistryOfInteriorService
{
    Task<CheckResult> CheckDocumentValidityAsync(
        DocumentNumber documentNumber,
        DocumentType type);
}
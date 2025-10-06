using DocuCheck.Domain.Entities.ChecksHistory.Enums;

namespace DocuCheck.Domain.Services.Interfaces;

public interface IDocumentService
{
     ResultType DetermineCheckResult(IReadOnlyCollection<CheckResult> partialResults);
}
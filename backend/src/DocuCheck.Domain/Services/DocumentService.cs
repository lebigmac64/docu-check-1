using DocuCheck.Domain.Entities.ChecksHistory.Enums;
using DocuCheck.Domain.Services.Interfaces;

namespace DocuCheck.Domain.Services;

internal class DocumentService : IDocumentService
{
    public ResultType DetermineCheckResult(IReadOnlyCollection<CheckResult> partialResults)
    {
        if (partialResults.Count <= 0 || partialResults.Any(r => r.ResultType == ResultType.Error))
            return ResultType.Error;

        return partialResults.Any(r => r.ResultType == ResultType.Invalid)
            ? ResultType.Invalid
            : ResultType.Valid;
    }
}
using DocuCheck.Application.Common;
using DocuCheck.Application.Repositories.Interfaces;
using DocuCheck.Application.Services.Interfaces;
using DocuCheck.Domain.Entities.ChecksHistory;
using DocuCheck.Domain.Entities.ChecksHistory.Enums;
using DocuCheck.Domain.Entities.ChecksHistory.ValueObjects;

namespace DocuCheck.Application.Services;

internal class DocumentService(
    IMinistryOfInteriorService ministryOfInteriorService, 
    ICheckHistoryRepository checkHistoryRepository) 
    : IDocumentService
{
    public async IAsyncEnumerable<CheckResult> CheckDocumentAsync(string documentNumber)
    {
        var docNumber = DocumentNumber.Create(documentNumber);

        var results = new List<CheckResult>();

        try
        {
            foreach (var documentType in Enum.GetValues<DocumentType>())
            {
                var checkResult = await ministryOfInteriorService.CheckDocumentValidityAsync(docNumber, documentType);
                results.Add(checkResult);
                yield return checkResult;
            }
        }
        finally
        {
            if (results.Count > 0)
            {
                var finalCheckResult = results.Any(r => r.ResultType == ResultType.Invalid)
                    ? ResultType.Invalid
                    : ResultType.Valid;

                var historyRecord = CheckHistory.Create(
                    DateTime.UtcNow,
                    docNumber,
                    finalCheckResult);

                await checkHistoryRepository.AddAsync(historyRecord);
            }
        }
    }

    public async Task<PagedResult<CheckHistory>> GetDocumentCheckHistoryAsync(int pageNumber, int pageSize)
    {
        if (pageNumber < 1)
            throw new ArgumentException("Page number must be at least 1", nameof(pageNumber));

        if (pageSize < 1)
            throw new ArgumentException("Page size must be between more than 1", nameof(pageSize));

        var pagedHistoryData = await checkHistoryRepository.GetCheckHistoryAsync(pageNumber, pageSize);
        
        return pagedHistoryData;
    }
}
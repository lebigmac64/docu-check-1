using DocuCheck.Application.Common;
using DocuCheck.Application.Handlers.Interfaces;
using DocuCheck.Application.Repositories.Interfaces;
using DocuCheck.Application.Services;
using DocuCheck.Domain.Entities.ChecksHistory;
using DocuCheck.Domain.Entities.ChecksHistory.Enums;
using DocuCheck.Domain.Entities.ChecksHistory.ValueObjects;
using DocuCheck.Domain.Services.Interfaces;

namespace DocuCheck.Application.Handlers;

internal class DocumentHandler(
    ICheckHistoryRepository checkHistoryRepository,
    IMinistryOfInteriorService ministryOfInteriorService,
    IDocumentService documentService)
    : IDocumentHandler
{
    public async IAsyncEnumerable<CheckResult> CheckDocumentAsync(string documentNumber)
    {
        var docNumber = DocumentNumber.Create(documentNumber);

        var proggressResults = new List<CheckResult>();

        var types = Enum.GetValues<DocumentType>();
        var tasks = types
            .Select(documentType => ministryOfInteriorService.CheckDocumentValidityAsync(
                docNumber, 
                documentType))
            .ToList();

        while (tasks.Count != 0)
        {
            var checkValidityResponse = await Task.WhenAny(tasks);
            
            proggressResults.Add(checkValidityResponse.Result);
        
            yield return checkValidityResponse.Result;
        
            tasks.Remove(checkValidityResponse);
        }

        var result = documentService.DetermineCheckResult(proggressResults);
        
        var historyRecord = CheckHistory.Create(
            DateTime.UtcNow,
            docNumber,
            result);

        await checkHistoryRepository.AddAsync(historyRecord);
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
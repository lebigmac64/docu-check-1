using DocuCheck.Application.Common;
using DocuCheck.Domain.Entities.ChecksHistory;
using DocuCheck.Domain.Entities.ChecksHistory.Enums;

namespace DocuCheck.Application.Services.Interfaces;

public interface IDocumentService
{
    IAsyncEnumerable<CheckResult> CheckDocumentAsync(string documentNumber);
    Task<PagedResult<CheckHistory>> GetDocumentCheckHistoryAsync(int pageNumber, int pageSize);
}
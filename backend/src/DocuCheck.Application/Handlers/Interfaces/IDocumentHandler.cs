using DocuCheck.Application.Common;
using DocuCheck.Domain.Entities.ChecksHistory;
using DocuCheck.Domain.Entities.ChecksHistory.Enums;

namespace DocuCheck.Application.Handlers.Interfaces;

public interface IDocumentHandler
{
    IAsyncEnumerable<CheckResult> CheckDocumentAsync(string documentNumber);
    Task<PagedResult<CheckHistory>> GetDocumentCheckHistoryAsync(int pageNumber, int pageSize);
}
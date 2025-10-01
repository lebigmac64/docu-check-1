using DocuCheck.Application.Common;
using DocuCheck.Domain.Entities.ChecksHistory;

namespace DocuCheck.Application.Repositories.Interfaces;

public interface ICheckHistoryRepository
{
    Task AddAsync(CheckHistory model);
    Task<PagedResult<CheckHistory>> GetCheckHistoryAsync(int pageNumber, int pageSize);
}
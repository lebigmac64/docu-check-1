using DocuCheck.Application.Common;
using DocuCheck.Application.Repositories.Interfaces;
using DocuCheck.Domain.Entities.ChecksHistory;
using Microsoft.EntityFrameworkCore;

namespace DocuCheck.Infrastructure.Persistence.Repositories;

internal class CheckHistoryRepository(DocuCheckDbContext context) : ICheckHistoryRepository
{
    public async Task AddAsync(CheckHistory model)
    {
        await context.CheckHistory.AddAsync(model);
        await context.SaveChangesAsync();
    }

    public async Task<PagedResult<CheckHistory>> GetCheckHistoryAsync(int pageNumber, int pageSize)
    {
        var query = context.CheckHistory.AsQueryable();

        var skip = pageNumber - 1;
        if (skip < 0) skip = 0;
        
        var totalCount = await query.CountAsync();
        
        var records = await query
            .OrderByDescending(ch => ch.CheckedAt)
            .Skip((skip) * pageSize)
            .Take(pageSize)
            .ToArrayAsync();
        
        return new PagedResult<CheckHistory>(
            records,
            new TotalCount(totalCount),
            new PageNumber(pageNumber),
            new PageSize(pageSize));
    }
}
using Marten;
using PaperBoy.ContentStore.Application.Projections;
using PaperBoy.ContentStore.Domain;
using PaperBoy.ContentStore.Shared;

namespace PaperBoy.ContentStore.Infrastructure;

public class PaperInfoRepository(IDocumentStore store) : IPaperInfoRepository
{
    public async Task<PagedResult<PaperInfo>> GetAllAsync(int page, int pageSize)
    {
        var session = store.LightweightSession();
        var count = await session.Query<PaperInfo>().CountAsync();

        var results = await session.Query<PaperInfo>()
            .OrderBy(x => x.DateCreated)
            .Skip(page * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new PagedResult<PaperInfo>(results, page, pageSize, count);
    }

    public async Task<PagedResult<PaperInfo>> GetPendingAsync(int page, int pageSize)
    {
        var session = store.LightweightSession();
        var count = await session.Query<PaperInfo>().CountAsync();

        var results = await session.Query<PaperInfo>()
            .Where(x =>
                x.Status == PaperStatus.Imported ||
                x.Status == PaperStatus.Summarized ||
                x.Status == PaperStatus.Scored)
            .OrderBy(x => x.DateCreated)
            .Skip(page * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new PagedResult<PaperInfo>(results, page, pageSize, count);
    }
}
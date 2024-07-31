using Marten;
using PaperBoy.ContentStore.Application.Projections;

namespace PaperBoy.ContentStore.Infrastructure;

public class PaperInfoRepository(IDocumentStore store) : IPaperInfoRepository
{
    public async Task<PagedResult<PaperInfo>> GetAllAsync(int page, int pageSize)
    {
        var session = store.LightweightSession();
        var count = await session.Query<PaperInfo>().CountAsync();
        var results = await session.Query<PaperInfo>().OrderBy(x => x.DateCreated).Skip(page * pageSize).Take(pageSize).ToListAsync();

        return new PagedResult<PaperInfo>(results, page, pageSize, count);
    }

    public Task<PaperInfo?> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<PagedResult<PaperInfo>> GetByStatusAsync(string[] statuses, int page, int pageSize)
    {
        var session = store.LightweightSession();
        var query = session.Query<PaperInfo>().AsQueryable();

        if (statuses.Length > 0)
        {
            query = query.Where(p => statuses.Contains(p.Status.ToString()));
        }

        var count = await query.CountAsync();
        var results = await query.OrderBy(x => x.DateCreated).Skip(page * pageSize).Take(pageSize).ToListAsync();

        return new PagedResult<PaperInfo>(results, page, pageSize, count);
    }
}

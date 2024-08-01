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

    public async Task<PagedResult<PaperInfo>> GetByStatusAsync(string status, int page, int pageSize)
    {
        var session = store.LightweightSession();
        var count = await session.Query<PaperInfo>().CountAsync(x => x.Status.ToString() == status);
        var results = await session.Query<PaperInfo>().Where(x => x.Status.ToString() == status).OrderBy(x => x.DateCreated).Skip(page * pageSize).Take(pageSize).ToListAsync();

        return new PagedResult<PaperInfo>(results, page, pageSize, count);
    }

    public Task<PaperInfo?> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}
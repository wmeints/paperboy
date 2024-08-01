using PaperBoy.ContentStore.Domain;
using PaperBoy.ContentStore.Shared;

namespace PaperBoy.ContentStore.Application.Projections;

public interface IPaperInfoRepository
{
    Task<PagedResult<PaperInfo>> GetAllAsync(int page, int pageSize);
    Task<PagedResult<PaperInfo>> GetPendingAsync(int page, int pageSize);
}

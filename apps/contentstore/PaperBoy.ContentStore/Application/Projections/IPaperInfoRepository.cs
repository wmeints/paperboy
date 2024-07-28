namespace PaperBoy.ContentStore.Application.Projections;

public interface IPaperInfoRepository
{
    Task<PaperInfo?> GetByIdAsync(Guid id);
    Task<PagedResult<PaperInfo>> GetAllAsync(int page, int pageSize);
}
using Base.Contracts.DAL;

namespace App.Contracts.DAL;

public interface IRegionRepository : IEntityRepository<App.DAL.DTO.Region>,
    IRegionCustom<App.DAL.DTO.Region>
{
}

public interface IRegionCustom<TEntity>
{
    Task<IEnumerable<TEntity>> GetAllAsync(Guid userId, bool noTracking = true);
    
}
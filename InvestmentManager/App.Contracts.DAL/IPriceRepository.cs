using Base.Contracts.DAL;

namespace App.Contracts.DAL;

public interface IPriceRepository : IEntityRepository<App.DAL.DTO.Price>,
    IPriceCustom<App.DAL.DTO.Price>
{
}

public interface IPriceCustom<TEntity>
{
    Task<IEnumerable<TEntity>> GetAllAsync(Guid userId, bool noTracking = true);
    
}
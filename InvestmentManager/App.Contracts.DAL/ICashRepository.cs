using Base.Contracts.DAL;

namespace App.Contracts.DAL;

public interface ICashRepository : IEntityRepository<App.DAL.DTO.Cash>,
    ICashCustom<App.DAL.DTO.Cash>
{
}

public interface ICashCustom<TEntity>
{
    Task<IEnumerable<TEntity>> GetAllAsync(Guid userId, bool noTracking = true);
    
}
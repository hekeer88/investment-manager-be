using Base.Contracts.DAL;

namespace App.Contracts.DAL;

public interface IStockRepository : IEntityRepository<App.DAL.DTO.Stock>,
    IStockCustom<App.DAL.DTO.Stock>
    
{
    // custom stuff here
}

public interface IStockCustom<TEntity>
{
    Task<IEnumerable<TEntity>> GetAllAsync(Guid userId, bool noTracking = true);
}



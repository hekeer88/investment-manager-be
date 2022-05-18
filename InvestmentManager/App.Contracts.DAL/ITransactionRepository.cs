using Base.Contracts.DAL;

namespace App.Contracts.DAL;

public interface ITransactionRepository : IEntityRepository<App.DAL.DTO.Transaction>,
    ITransactionCustom<App.DAL.DTO.Transaction>
{
}

public interface ITransactionCustom<TEntity>
{
    Task<IEnumerable<TEntity>> GetAllAsync(Guid userId, bool noTracking = true);
    
}
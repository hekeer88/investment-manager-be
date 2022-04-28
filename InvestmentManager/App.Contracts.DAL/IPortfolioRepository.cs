using Base.Contracts.DAL;

namespace App.Contracts.DAL;

public interface IPortfolioRepository : IEntityRepository<App.DAL.DTO.Portfolio>, IPortfolioCustom<App.DAL.DTO.Portfolio>
{
}

// TODO: teised repod ka selliseks
public interface IPortfolioCustom<TEntity>
{
    Task<IEnumerable<TEntity>> GetAllByNameAsync(string name, bool noTracking = true);
    
    Task<IEnumerable<TEntity>> GetAllAsync(Guid userId, bool noTracking = true);
}
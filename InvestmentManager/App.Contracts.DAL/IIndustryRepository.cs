using Base.Contracts.DAL;

namespace App.Contracts.DAL;

public interface IIndustryRepository : IEntityRepository<App.DAL.DTO.Industry>,
    IIndustryCustom<App.DAL.DTO.Industry>
{
}

public interface IIndustryCustom<TEntity>
{
    Task<IEnumerable<TEntity>> GetAllAsync(Guid userId, bool noTracking = true);
    
}
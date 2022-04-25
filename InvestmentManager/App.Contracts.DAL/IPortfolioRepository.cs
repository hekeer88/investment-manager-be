using Base.Contracts.DAL;

namespace App.Contracts.DAL;

public interface IPortfolioRepository : IEntityRepository<App.DAL.DTO.Portfolio>
{
    Task<IEnumerable<App.DAL.DTO.Portfolio>> GetAllByNameAsync(string name, bool noTracking = true);
    
    Task<IEnumerable<App.DAL.DTO.Portfolio>> GetAllAsync(Guid userId, bool noTracking = true);
}
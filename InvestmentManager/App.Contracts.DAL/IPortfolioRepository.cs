using App.Domain;
using Base.Contracts.DAL;

namespace App.Contracts.DAL;

public interface IPortfolioRepository : IEntityRepository<Portfolio>
{
    Task<IEnumerable<Portfolio>> GetAllByNameAsync(string name, bool noTracking = true);
}
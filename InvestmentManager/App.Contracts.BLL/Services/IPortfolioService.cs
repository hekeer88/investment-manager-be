using App.Contracts.DAL;
using App.Public.DTO.v1;
using Base.Contracts.BLL;

namespace App.Contracts.BLL.Services;

public interface IPortfolioService :  IEntityService<App.Public.DTO.v1.Portfolio, App.BLL.DTO.Portfolio>, 
    IPortfolioCustom<App.BLL.DTO.Portfolio>
{
    Task<IEnumerable<App.Public.DTO.v1.Portfolio>> PublicGetAllAsync(Guid userId, bool noTracking = true);
    Task<App.Public.DTO.v1.Portfolio?> PublicFirstOrDefaultAsync(Guid portfolioId, bool noTracking = true);
} 
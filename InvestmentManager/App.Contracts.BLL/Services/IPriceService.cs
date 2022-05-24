using App.Contracts.DAL;
using App.Public.DTO.v1;
using Base.Contracts.BLL;

namespace App.Contracts.BLL.Services;

public interface IPriceService :  IEntityService<App.Public.DTO.v1.Price, App.BLL.DTO.Price>, 
    IIndustryCustom<App.BLL.DTO.Price>
{
    Task<IEnumerable<App.Public.DTO.v1.Price>> PublicGetAllAsync(Guid userId, bool noTracking = true);
    // Task<App.Public.DTO.v1.Region?> PublicFirstOrDefaultAsync(Guid portfolioId, bool noTracking = true);
} 
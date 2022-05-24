using App.Contracts.DAL;
using App.Public.DTO.v1;
using Base.Contracts.BLL;

namespace App.Contracts.BLL.Services;

public interface IIndustryService :  IEntityService<App.Public.DTO.v1.Industry, App.BLL.DTO.Industry>, 
    IIndustryCustom<App.BLL.DTO.Industry>
{
    Task<IEnumerable<App.Public.DTO.v1.Industry>> PublicGetAllAsync(Guid userId, bool noTracking = true);
    // Task<App.Public.DTO.v1.Region?> PublicFirstOrDefaultAsync(Guid portfolioId, bool noTracking = true);
} 
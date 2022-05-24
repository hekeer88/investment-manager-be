using App.Contracts.DAL;
using App.Public.DTO.v1;
using Base.Contracts.BLL;

namespace App.Contracts.BLL.Services;

public interface IRegionService :  IEntityService<App.Public.DTO.v1.Region, App.BLL.DTO.Region>, 
    IRegionCustom<App.BLL.DTO.Region>
{
    Task<IEnumerable<App.Public.DTO.v1.Region>> PublicGetAllAsync(Guid userId, bool noTracking = true);
    Task<App.Public.DTO.v1.Region?> PublicFirstOrDefaultAsync(Guid regionId, bool noTracking = true);
} 
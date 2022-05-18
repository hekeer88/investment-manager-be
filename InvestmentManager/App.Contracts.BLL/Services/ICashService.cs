using App.Contracts.DAL;
using App.Public.DTO.v1;
using Base.Contracts.BLL;

namespace App.Contracts.BLL.Services;

public interface ICashService :  IEntityService<App.Public.DTO.v1.Cash, App.BLL.DTO.Cash>, 
    ICashCustom<App.BLL.DTO.Cash>
{
    Task<IEnumerable<App.Public.DTO.v1.Cash>> PublicGetAllAsync(Guid userId, bool noTracking = true);
    Task<App.Public.DTO.v1.Cash?> PublicFirstOrDefaultAsync(Guid CashId, bool noTracking = true);
} 
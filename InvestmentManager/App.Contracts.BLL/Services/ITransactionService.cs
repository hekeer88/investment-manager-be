using App.Contracts.DAL;
using App.Public.DTO.v1;
using Base.Contracts.BLL;

namespace App.Contracts.BLL.Services;

public interface ITransactionService :  IEntityService<App.Public.DTO.v1.Transaction, App.BLL.DTO.Transaction>, 
    ITransactionCustom<App.BLL.DTO.Transaction>
{
    Task<IEnumerable<App.Public.DTO.v1.Transaction>> PublicGetAllAsync(Guid userId, bool noTracking = true);
    // Task<App.Public.DTO.v1.Portfolio?> PublicFirstOrDefaultAsync(Guid portfolioId, bool noTracking = true);
} 
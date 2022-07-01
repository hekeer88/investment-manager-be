using App.Contracts.DAL;
using App.Public.DTO.v1;
using Base.Contracts.BLL;
using Transaction = App.BLL.DTO.Transaction;

namespace App.Contracts.BLL.Services;

public interface ITransactionService :  IEntityService<App.Public.DTO.v1.Transaction, App.BLL.DTO.Transaction>, 
    ITransactionCustom<App.BLL.DTO.Transaction>
{
    Task<IEnumerable<App.Public.DTO.v1.Transaction>> PublicGetAllAsync(Guid userId, bool noTracking = true);
    Task<App.Public.DTO.v1.Transaction?> PublicFirstOrDefaultAsync(Guid transactionId, bool noTracking = true);
    Public.DTO.v1.Transaction? PublicAdd(App.Public.DTO.v1.Transaction entity);

}   
using App.BLL.DTO;
using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using App.DAL.EF.Mappers;
using Base.BLL;
using Base.Contracts.Base;
using Portfolio = App.Public.DTO.v1.Portfolio;
using PortfolioMapper = App.Public.DTO.Mappers.PortfolioMapper;
using Transaction = App.Public.DTO.v1.Transaction;

namespace App.BLL.Services;

public class TransactionService: BaseEntityService<App.Public.DTO.v1.Transaction, App.BLL.DTO.Transaction, App.DAL.DTO.Transaction, 
        ITransactionRepository>, 
    ITransactionService
{
    public TransactionService(ITransactionRepository repository, IMapper<App.BLL.DTO.Transaction, DAL.DTO.Transaction> bllMapper,
        IMapper<App.Public.DTO.v1.Transaction, App.BLL.DTO.Transaction> publicMapper) : base(repository, bllMapper, publicMapper)
    {
    }

    public async Task<IEnumerable<App.BLL.DTO.Transaction>> GetAllAsync(Guid userId, bool noTracking = true)
    {

        
        var res =
        (await Repository.GetAllAsync(userId, noTracking)).Select(x => BLLMapper.Map(x)!).ToList();

        return res;
    }

    public async Task<IEnumerable<Public.DTO.v1.Transaction>> PublicGetAllAsync(Guid userId, bool noTracking = true)
    {
        var res =
            (await GetAllAsync(userId, noTracking)).Select(x => PublicMapper.Map(x)!).ToList();
        
        return res;
    }

    public async Task<Transaction?> PublicFirstOrDefaultAsync(Guid transactionId, bool noTracking = true)
    {
        var res = BLLMapper.Map(await Repository.FirstOrDefaultAsync(transactionId, noTracking));
        return PublicMapper.Map(res);
    }
}
using App.BLL.DTO;
using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using App.DAL.EF.Mappers;
using Base.BLL;
using Base.Contracts.Base;

namespace App.BLL.Services;


public class StockService: BaseEntityService<App.Public.DTO.v1.Stock, 
        App.BLL.DTO.Stock, App.DAL.DTO.Stock, IStockRepository>,
    IStockService
{
    public StockService(IStockRepository repository, IMapper<Stock, DAL.DTO.Stock> bllMapper,
        IMapper<App.Public.DTO.v1.Stock, Stock> publicMapper) : base(repository, bllMapper, publicMapper)
    {
    }

    public async Task<IEnumerable<Stock>> GetAllAsync(Guid portfolioId, bool noTracking = true)
    {
        var res =
            (await Repository.GetAllAsync(portfolioId, noTracking)).Select(x => BLLMapper.Map(x)!).ToList();

        foreach (var stock in res)
        {
            CalculateStockBalance(stock);
            stock.Ticker = stock.Ticker.ToUpper();
        }

        return res;
    }
    
    public async Task<IEnumerable<Public.DTO.v1.Stock>> PublicGetAllAsync(Guid userId, bool noTracking = true)
    {
        var res =
            (await GetAllAsync(userId, noTracking)).Select(x => PublicMapper.Map(x)!).ToList();
        
        return res;
    }
    
    public async Task<Public.DTO.v1.Stock?> PublicFirstOrDefaultAsync(Guid stockId, bool noTracking = true)
    {
        var res = BLLMapper.Map(await Repository.FirstOrDefaultAsync(stockId, noTracking));
        return PublicMapper.Map(res);
    }
    
    private void CalculateStockBalance(Stock stock)
    {
        // TODO: sum only "BUY" transactions
        var quantity = stock.Transactions?.Sum(t => t.Quantity) ?? 0;
        var lastPrice = stock.Prices?.OrderByDescending(p => p.PriceTime).FirstOrDefault()?.CurrentPrice ?? 0;
        if (lastPrice == 0)
        {
            lastPrice =
                stock.Transactions?.OrderByDescending(t => t.TransactionDate).FirstOrDefault()?.TransactionPrice ?? 0;
        }
        stock.Balance = quantity * lastPrice;
    }
}
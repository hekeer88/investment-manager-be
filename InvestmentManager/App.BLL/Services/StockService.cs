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
            stock.Company = stock.Company.ToUpper();
        }

        return res;
    }
}
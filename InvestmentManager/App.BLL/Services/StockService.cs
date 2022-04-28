using App.BLL.DTO;
using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using App.DAL.EF.Mappers;
using Base.BLL;
using Base.Contracts.Base;

namespace App.BLL.Services;

public class StockService: BaseEntityService<App.BLL.DTO.Stock, App.DAL.DTO.Stock, IStockRepository>, IStockService
{
    public StockService(IStockRepository repository, IMapper<Stock, DAL.DTO.Stock> mapper) : base(repository, mapper)
    {
    }
}
using App.Contracts.DAL;
using App.DAL.DTO;
using Base.Contracts.BLL;

namespace App.Contracts.BLL.Services;

public interface IStockService : IEntityService<App.Public.DTO.v1.Stock, App.BLL.DTO.Stock>, IStockCustom<App.BLL.DTO.Stock>
{
    
}
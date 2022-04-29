using App.Contracts.DAL;
using App.DAL.DTO;
using Base.Contracts.BLL;

namespace App.Contracts.BLL.Services;

public interface IStockService : IEntityService<App.BLL.DTO.Stock>, IStockCustom<App.BLL.DTO.Stock>
{
    
}
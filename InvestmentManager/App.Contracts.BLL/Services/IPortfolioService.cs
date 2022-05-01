using App.Contracts.DAL;
using Base.Contracts.BLL;

namespace App.Contracts.BLL.Services;

public interface IPortfolioService :  IEntityService<App.BLL.DTO.Portfolio>, 
    IPortfolioCustom<App.BLL.DTO.Portfolio>
{
   
}
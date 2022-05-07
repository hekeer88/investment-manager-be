using App.Contracts.DAL;
using App.Public.DTO.v1;
using Base.Contracts.BLL;

namespace App.Contracts.BLL.Services;

public interface IPortfolioService :  IEntityService<App.Public.DTO.v1.Portfolio, App.BLL.DTO.Portfolio>, 
    IPortfolioCustom<App.BLL.DTO.Portfolio>
{
}
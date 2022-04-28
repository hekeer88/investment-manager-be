using App.Contracts.DAL;
using Base.Contracts.BLL;

namespace App.Contracts.BLL.Services;

// TODO: teised serivced ka selliseks
public interface IPortfolioService :  IEntityService<App.BLL.DTO.Portfolio>, IPortfolioCustom<App.BLL.DTO.Portfolio>
{
    
}
using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using Base.Contracts.BLL;

namespace App.Contracts.BLL;


public interface IAppBLL : IBLL
{
    IPortfolioService Portfolios { get; }
    IStockService Stocks { get; }
    ILoanService Loans { get; }
}
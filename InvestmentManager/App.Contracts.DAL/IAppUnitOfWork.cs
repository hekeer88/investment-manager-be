using App.BLL.DTO;
using Base.Contracts.DAL;

namespace App.Contracts.DAL;

public interface IAppUnitOfWork : IUnitOfWork
{
    IPortfolioRepository Portfolios { get; }
    IStockRepository Stocks { get; }
    ILoanRepository Loans { get; }
    ICashRepository Cashes { get; }
    IIndustryRepository Industries { get; }
    IRegionRepository Regions { get; }
    IPriceRepository Prices { get; }
    ITransactionRepository Transactions { get; }
}
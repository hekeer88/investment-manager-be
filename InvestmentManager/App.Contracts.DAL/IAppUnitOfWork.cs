using Base.Contracts.DAL;

namespace App.Contracts.DAL;

public interface IAppUnitOfWork : IUnitOfWork
{
    IPortfolioRepository Portfolios { get; }
    IStockRepository Stocks { get; }
}
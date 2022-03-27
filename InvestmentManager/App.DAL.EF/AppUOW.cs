using App.Contracts.DAL;
using App.DAL.EF.Repositories;
using Base.Contracts.DAL;
using Base.DAL.EF;

namespace App.DAL.EF;

public class AppUOW : BaseUOW<AppDbContext>, IAppUnitOfWork
{
    
    public AppUOW(AppDbContext dbContext) : base(dbContext)
    {
    }
    
    private  IPortfolioRepository? _portfolios;
    public virtual IPortfolioRepository Portfolios => _portfolios ??= new PortfolioRepository(UOWDbContext);
    
    // selliselt teha k6ik repod
    private  IStockRepository? _stocks;
    public virtual IStockRepository Stocks => _stocks ??= new StockRepository(UOWDbContext);
    
    private  ILoanRepository? _loans;
    public virtual ILoanRepository Loans => _loans ??= new LoanRepository(UOWDbContext);

    
}
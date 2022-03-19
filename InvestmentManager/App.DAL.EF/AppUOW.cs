using App.Contracts.DAL;
using App.DAL.EF.Repositories;
using Base.Contracts.DAL;

namespace App.DAL.EF;

public class AppUOW : IAppUnitOfWork
{

    protected readonly AppDbContext UOWDbContext;

    public AppUOW(AppDbContext uowDbContext)
    {
        UOWDbContext = uowDbContext;
    }
    
    
    
    
    
    public virtual async Task<int> SaveChangesAsync()
    {
        return await UOWDbContext.SaveChangesAsync();
    }

    public virtual int SaveChanges()
    {
        return UOWDbContext.SaveChanges();
    }

    public virtual IPortfolioRepository Portfolios => new PortfolioRepository(UOWDbContext);
    public virtual IStockRepository Stocks { get; }
}
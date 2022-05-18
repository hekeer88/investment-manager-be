using App.Contracts.DAL;
using App.DAL.EF.Mappers;
using App.DAL.EF.Repositories;
using Base.Contracts.DAL;
using Base.DAL.EF;

namespace App.DAL.EF;

public class AppUOW : BaseUOW<AppDbContext>, IAppUnitOfWork
{
    
    private readonly AutoMapper.IMapper _mapper;
    
    public AppUOW(AppDbContext dbContext, AutoMapper.IMapper mapper) : base(dbContext)
    {
        _mapper = mapper;
    }

    private  IPortfolioRepository? _portfolios;
    public virtual IPortfolioRepository Portfolios => _portfolios ??= new PortfolioRepository(UOWDbContext, new PortfolioMapper(_mapper));
    
    private  IStockRepository? _stocks;
    public virtual IStockRepository Stocks => _stocks ??= new StockRepository(UOWDbContext, new StockMapper(_mapper));
    
    private  ILoanRepository? _loans;
    public virtual ILoanRepository Loans => _loans ??= new LoanRepository(UOWDbContext, new LoanMapper(_mapper));

    private ICashRepository? _cash;
    public virtual ICashRepository Cashes => _cash ??= new CashRepository(UOWDbContext, new CashMapper(_mapper));
    
    public IIndustryRepository Industries { get; }
    public IRegionRepository Regions { get; }
    public IPriceRepository Prices { get; }
    public ITransactionRepository Transactions { get; }
}
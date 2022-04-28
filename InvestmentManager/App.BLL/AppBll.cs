using App.BLL.Mappers;
using App.BLL.Services;
using App.Contracts.BLL;
using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using AutoMapper;
using Base.BLL;
using Base.Contracts.Base;
using LoanMapper = App.DAL.EF.Mappers.LoanMapper;
using StockMapper = App.DAL.EF.Mappers.StockMapper;

namespace App.BLL;

public class AppBLL : BaseBll<IAppUnitOfWork>, IAppBLL
{
    protected IAppUnitOfWork UnitOfWork;
    private readonly AutoMapper.IMapper _mapper;

    public AppBLL(IAppUnitOfWork unitOfWork, IMapper mapper)
    {
        UnitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public override async Task<int> SaveChangesAsync()
    {
        return await UnitOfWork.SaveChangesAsync();
    }

    public override int SaveChanges()
    {
        return UnitOfWork.SaveChanges();
    }
    
    private IPortfolioService? _portfolios;
    
    public IPortfolioService Portfolios =>
        _portfolios ??= new PortfolioService(UnitOfWork.Portfolios, new PortfolioMapper(_mapper));
    
    
    private IStockService? _socks;
    public IStockService Stocks =>
        _socks ??= new StockService(UnitOfWork.Stocks, new Mappers.StockMapper(_mapper));
    
    private ILoanService? _loans;
    public ILoanService Loans =>
        _loans ??= new LoanService(UnitOfWork.Loans, new Mappers.LoanMapper(_mapper));
    
}
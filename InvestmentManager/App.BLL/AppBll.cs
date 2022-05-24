using App.BLL.Mappers;
using App.BLL.Services;
using App.Contracts.BLL;
using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using App.Public.DTO;
using AutoMapper;
using Base.BLL;
using Base.Contracts.Base;
using Base.DAL;
using LoanMapper = App.DAL.EF.Mappers.LoanMapper;
using StockMapper = App.DAL.EF.Mappers.StockMapper;

namespace App.BLL;

public class AppBLL : BaseBll<IAppUnitOfWork>, IAppBLL
{
    protected IAppUnitOfWork UnitOfWork;
    private readonly AutoMapper.IMapper _bllMapper;
    private readonly AutoMapper.IMapper _publicMapper;

    public AppBLL(IAppUnitOfWork unitOfWork, IMapper bllBllMapper, IMapper publicMapper)
    {
        UnitOfWork = unitOfWork;
        _bllMapper = bllBllMapper;
        _publicMapper = publicMapper;
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
        _portfolios ??= new PortfolioService(
            UnitOfWork.Portfolios, 
            new PortfolioMapper(_bllMapper),
            new Public.DTO.Mappers.PortfolioMapper(_publicMapper));
    
    private IStockService? _socks;
    public IStockService Stocks =>
        _socks ??= new StockService(
            UnitOfWork.Stocks, 
            new Mappers.StockMapper(_bllMapper),
            new Public.DTO.Mappers.StockMapper(_publicMapper));
    
    private ILoanService? _loans;
    public ILoanService Loans =>
        _loans ??= new LoanService(
            UnitOfWork.Loans, 
            new Mappers.LoanMapper(_bllMapper),
            new Public.DTO.Mappers.LoanMapper(_publicMapper));
    
    private ICashService? _cash;
    public ICashService Cashes =>
        _cash ??= new CashService(
            UnitOfWork.Cashes, 
            new Mappers.CashMapper(_bllMapper),
            new Public.DTO.Mappers.CashMapper(_publicMapper));

    private IRegionService? _region;
    public IRegionService Regions =>
        _region ??= new RegionService(
            UnitOfWork.Regions, 
            new Mappers.RegionMapper(_bllMapper),
            new Public.DTO.Mappers.RegionMapper(_publicMapper));
    
    
    private IIndustryService? _industry;
    public IIndustryService Industries =>
        _industry ??= new IndustryService(
            UnitOfWork.Industries, 
            new Mappers.IndustryMapper(_bllMapper),
            new Public.DTO.Mappers.IndustryMapper(_publicMapper));
    
    private IPriceService? _price;
    public IPriceService Prices =>
        _price ??= new PriceService(
            UnitOfWork.Prices, 
            new Mappers.PriceMapper(_bllMapper),
            new Public.DTO.Mappers.PriceMapper(_publicMapper));
    
    
    private ITransactionService? _transaction;
    public ITransactionService Transactions =>
        _transaction ??= new TransactionService(
            UnitOfWork.Transactions, 
            new Mappers.TransactionMapper(_bllMapper),
            new Public.DTO.Mappers.TransactionMapper(_publicMapper));
    
    
    
}
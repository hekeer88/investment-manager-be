using App.Contracts.BLL;
using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using App.DAL.EF.Mappers;
using Base.BLL;
using Base.Contracts.Base;

namespace App.BLL;

public class AppBLL : BaseBll<IAppUnitOfWork>, IAppBLL
{
    protected IAppUnitOfWork UnitOfWork;
    private readonly AutoMapper.IMapper _mapper;

    public AppBLL(IAppUnitOfWork unitOfWork, IMapper<,> mapper)
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

    private IMeetingPortfolio? _meetings;

    public IMeetingService Meetings =>
        _meetings ??= new MeetingService(UnitOfWork.Meetings, new MeetingMapper(_mapper));


    private IMeetingOptionService? _meetingOptions;

    public IMeetingOptionService MeetingOptions =>
        _meetingOptions ??= new MeetingOptionService(UnitOfWork.MeetingOptions, new MeetingOptionMapper(_mapper));

    
    
    private IPortfolioService? _portfolios;
    public IPortfolioService Portfolios =>
        _portfolios ??= new PortfolioService(UnitOfWork.Portfolios, new PortfolioMapper(_mapper));
    
    
    private IStockService? _socks;
    private ILoanService? _loans;
}
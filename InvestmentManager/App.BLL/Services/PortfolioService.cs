using App.BLL.DTO;
using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using App.DAL.EF.Mappers;
using Base.BLL;
using Base.Contracts.Base;
using PortfolioMapper = App.Public.DTO.Mappers.PortfolioMapper;

namespace App.BLL.Services;

public class PortfolioService: BaseEntityService<App.Public.DTO.v1.Portfolio, App.BLL.DTO.Portfolio, App.DAL.DTO.Portfolio, IPortfolioRepository>, 
    IPortfolioService
{
    public PortfolioService(IPortfolioRepository repository, IMapper<Portfolio, DAL.DTO.Portfolio> bllMapper,
        IMapper<App.Public.DTO.v1.Portfolio, Portfolio> publicMapper) : base(repository, bllMapper, publicMapper)
    {
    }
    
    public async Task<IEnumerable<App.BLL.DTO.Portfolio>> GetAllByNameAsync(string name, bool noTracking = true)
    {
        return (await Repository.GetAllByNameAsync(name, noTracking)).Select(x => BLLMapper.Map(x)!);
    }

    public async Task<IEnumerable<Portfolio>> GetAllAsync(Guid userId, bool noTracking = true)
    {
        
        // business logic siin, nt nagu see n2ide mis teeb portfoli nime suureks
        
        var res =
        (await Repository.GetAllAsync(userId, noTracking)).Select(x => BLLMapper.Map(x)!).ToList();
    
        foreach (var portfolio in res)
        {
            portfolio.Name = portfolio.Name.ToUpper();
        }
    
        return res;
    }

    public async Task<IEnumerable<Public.DTO.v1.Portfolio>> GetAllAsyncPublic(Guid userId, bool noTracking = true)
    {
        var res =
            (await GetAllAsync(userId, noTracking)).Select(x => PublicMapper.Map(x)!).ToList();
        
        return res;
    }







}
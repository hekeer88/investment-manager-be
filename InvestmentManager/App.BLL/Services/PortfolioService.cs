using App.BLL.DTO;
using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using App.DAL.EF.Mappers;
using Base.BLL;
using Base.Contracts.Base;
using Portfolio = App.Public.DTO.v1.Portfolio;
using PortfolioMapper = App.Public.DTO.Mappers.PortfolioMapper;

namespace App.BLL.Services;

public class PortfolioService: BaseEntityService<App.Public.DTO.v1.Portfolio, App.BLL.DTO.Portfolio, App.DAL.DTO.Portfolio, 
        IPortfolioRepository>, 
    IPortfolioService
{
    public PortfolioService(IPortfolioRepository repository, IMapper<App.BLL.DTO.Portfolio, DAL.DTO.Portfolio> bllMapper,
        IMapper<App.Public.DTO.v1.Portfolio, App.BLL.DTO.Portfolio> publicMapper) : base(repository, bllMapper, publicMapper)
    {
    }
    
    public async Task<IEnumerable<App.BLL.DTO.Portfolio>> GetAllByNameAsync(string name, bool noTracking = true)
    {
        return (await Repository.GetAllByNameAsync(name, noTracking)).Select(x => BLLMapper.Map(x)!);
    }


    public async Task<IEnumerable<App.BLL.DTO.Portfolio>> GetAllAsync(Guid userId, bool noTracking = true)
    {

        var res =
        (await Repository.GetAllAsync(userId, noTracking)).Select(x => BLLMapper.Map(x)!).ToList();

        return res;
    }

    public async Task<IEnumerable<Public.DTO.v1.Portfolio>> PublicGetAllAsync(Guid userId, bool noTracking = true)
    {
        var res =
            (await GetAllAsync(userId, noTracking)).Select(x => PublicMapper.Map(x)!).ToList();
        
        return res;
    }

    public async Task<Public.DTO.v1.Portfolio?> PublicFirstOrDefaultAsync(Guid portfolioId, bool noTracking = true)
    {
        var res = BLLMapper.Map(await Repository.FirstOrDefaultAsync(portfolioId, noTracking));
        return PublicMapper.Map(res);
    }
}
using App.BLL.DTO;
using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using App.DAL.EF.Mappers;
using Base.BLL;
using Base.Contracts.Base;
using Portfolio = App.Public.DTO.v1.Portfolio;
using PortfolioMapper = App.Public.DTO.Mappers.PortfolioMapper;

namespace App.BLL.Services;

public class RegionService: BaseEntityService<App.Public.DTO.v1.Region, App.BLL.DTO.Region, App.DAL.DTO.Region, 
        IRegionRepository>, 
    IRegionService
{
    public RegionService(IRegionRepository repository, IMapper<App.BLL.DTO.Region, DAL.DTO.Region> bllMapper,
        IMapper<App.Public.DTO.v1.Region, App.BLL.DTO.Region> publicMapper) : base(repository, bllMapper, publicMapper)
    {
    }
    
    public async Task<IEnumerable<App.BLL.DTO.Region>> GetAllAsync(Guid userId, bool noTracking = true)
    {
        
        // business logic siin, nt nagu see n2ide mis teeb portfoli nime suureks
        
        var res =
        (await Repository.GetAllAsync(userId, noTracking)).Select(x => BLLMapper.Map(x)!).ToList();
    
        // foreach (var portfolio in res)
        // {
        //     portfolio.Name = portfolio.Name.ToUpper();
        // }
    
        return res;
    }

    public async Task<IEnumerable<Public.DTO.v1.Region>> PublicGetAllAsync(Guid userId, bool noTracking = true)
    {
        var res =
            (await GetAllAsync(userId, noTracking)).Select(x => PublicMapper.Map(x)!).ToList();
        
        return res;
    }
    
    public async Task<Public.DTO.v1.Region?> PublicFirstOrDefaultAsync(Guid regionId, bool noTracking = true)
    {
        var res = BLLMapper.Map(await Repository.FirstOrDefaultAsync(regionId, noTracking));
        return PublicMapper.Map(res);
    }
}
using App.BLL.DTO;
using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using App.DAL.EF.Mappers;
using Base.BLL;
using Base.Contracts.Base;
using Industry = App.Public.DTO.v1.Industry;
using Portfolio = App.Public.DTO.v1.Portfolio;
using PortfolioMapper = App.Public.DTO.Mappers.PortfolioMapper;

namespace App.BLL.Services;

public class IndustryService: BaseEntityService<App.Public.DTO.v1.Industry, App.BLL.DTO.Industry, App.DAL.DTO.Industry, 
        IIndustryRepository>, 
    IIndustryService
{
    public IndustryService(IIndustryRepository repository, IMapper<App.BLL.DTO.Industry, DAL.DTO.Industry> bllMapper,
        IMapper<App.Public.DTO.v1.Industry, App.BLL.DTO.Industry> publicMapper) : base(repository, bllMapper, publicMapper)
    {
    }
    

    public async Task<IEnumerable<App.BLL.DTO.Industry>> GetAllAsync(Guid userId, bool noTracking = true)
    {
        
        var res =
        (await Repository.GetAllAsync(userId, noTracking)).Select(x => BLLMapper.Map(x)!).ToList();
        
        return res;
    }

    public async Task<IEnumerable<Public.DTO.v1.Industry>> PublicGetAllAsync(Guid userId, bool noTracking = true)
    {
        var res =
            (await GetAllAsync(userId, noTracking)).Select(x => PublicMapper.Map(x)!).ToList();
        
        return res;
    }
    

    public async Task<Industry?> PublicFirstOrDefaultAsync(Guid industryId, bool noTracking = true)
    {
        var res = BLLMapper.Map(await Repository.FirstOrDefaultAsync(industryId, noTracking));
        return PublicMapper.Map(res);
    }
}
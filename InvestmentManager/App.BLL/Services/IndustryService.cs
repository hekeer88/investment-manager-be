using App.BLL.DTO;
using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using App.DAL.EF.Mappers;
using Base.BLL;
using Base.Contracts.Base;
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
        
        // business logic siin, nt nagu see n2ide mis teeb portfoli nime suureks
        
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

}
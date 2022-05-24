using App.BLL.DTO;
using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using App.DAL.EF.Mappers;
using Base.BLL;
using Base.Contracts.Base;
using Portfolio = App.Public.DTO.v1.Portfolio;
using PortfolioMapper = App.Public.DTO.Mappers.PortfolioMapper;

namespace App.BLL.Services;

public class PriceService: BaseEntityService<App.Public.DTO.v1.Price, App.BLL.DTO.Price, App.DAL.DTO.Price, 
        IPriceRepository>, 
    IPriceService
{
    public PriceService(IPriceRepository repository, IMapper<App.BLL.DTO.Price, DAL.DTO.Price> bllMapper,
        IMapper<App.Public.DTO.v1.Price, App.BLL.DTO.Price> publicMapper) : base(repository, bllMapper, publicMapper)
    {
    }

    public async Task<IEnumerable<App.BLL.DTO.Price>> GetAllAsync(Guid userId, bool noTracking = true)
    {
        var res =
        (await Repository.GetAllAsync(userId, noTracking)).Select(x => BLLMapper.Map(x)!).ToList();

        return res;
    }

    public async Task<IEnumerable<Public.DTO.v1.Price>> PublicGetAllAsync(Guid userId, bool noTracking = true)
    {
        var res =
            (await GetAllAsync(userId, noTracking)).Select(x => PublicMapper.Map(x)!).ToList();
        
        return res;
    }

}
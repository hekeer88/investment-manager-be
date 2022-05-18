using App.BLL.DTO;
using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using Base.BLL;
using Base.Contracts.Base;

namespace App.BLL.Services;


public class CashService: BaseEntityService<App.Public.DTO.v1.Cash, App.BLL.DTO.Cash, App.DAL.DTO.Cash, ICashRepository>, 
    ICashService
{
    public CashService(ICashRepository repository, IMapper<Cash, DAL.DTO.Cash> bllMapper,
        IMapper<App.Public.DTO.v1.Cash, Cash> publicMapper) : base(repository, bllMapper, publicMapper)
    {
    }

    public async Task<IEnumerable<App.BLL.DTO.Cash>> GetAllAsync(Guid userId, bool noTracking = true)
    {
        var res =
            (await Repository.GetAllAsync(userId, noTracking)).Select(x => BLLMapper.Map(x)!).ToList();
        
        return res;

    }

    public async Task<IEnumerable<Public.DTO.v1.Cash>> PublicGetAllAsync(Guid userId, bool noTracking = true)
    {
        var res =
            (await GetAllAsync(userId)).Select(l => PublicMapper.Map(l)!).ToList();
        
        return res;
    }

    public async Task<Public.DTO.v1.Cash?> PublicFirstOrDefaultAsync(Guid id, bool noTracking = true)
    {
        var res = BLLMapper.Map(await Repository.FirstOrDefaultAsync(id, noTracking));
        return PublicMapper.Map(res);
    }
}
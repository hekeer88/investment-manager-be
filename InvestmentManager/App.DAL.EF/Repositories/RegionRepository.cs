using App.Contracts.DAL;
using Base.Contracts.Base;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;


namespace App.DAL.EF.Repositories;

public class RegionRepository : BaseEntityRepository<App.DAL.DTO.Region, App.Domain.Region, AppDbContext>, IRegionRepository
{
    public RegionRepository(AppDbContext dbContext, IMapper<App.DAL.DTO.Region, App.Domain.Region> mapper) : base(dbContext, mapper)
    {
    }


    public async Task<IEnumerable<App.DAL.DTO.Region>> GetAllAsync(Guid userId, bool noTracking = true)
    {
        var query = CreateQuery(noTracking);
        
        return (await query.ToListAsync()).Select(x=>Mapper.Map(x)!);
    }
    
}
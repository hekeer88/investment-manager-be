using App.Contracts.DAL;
using Base.Contracts.Base;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;


namespace App.DAL.EF.Repositories;

public class IndustryRepository : BaseEntityRepository<App.DAL.DTO.Industry, App.Domain.Industry, AppDbContext>, IIndustryRepository
{
    public IndustryRepository(AppDbContext dbContext, IMapper<App.DAL.DTO.Industry, App.Domain.Industry> mapper) : base(dbContext, mapper)
    {
    }


    public async Task<IEnumerable<App.DAL.DTO.Industry>> GetAllAsync(Guid userId, bool noTracking = true)
    {
        var query = CreateQuery(noTracking);

        return (await query.ToListAsync()).Select(x=>Mapper.Map(x)!);
    }
    
}
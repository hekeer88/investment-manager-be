using App.Contracts.DAL;
using App.Public.DTO;
using App.Public.DTO.v1;
using AutoMapper;
using Base.Contracts.Base;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class PriceRepository : BaseEntityRepository<App.DAL.DTO.Price, App.Domain.Price, AppDbContext>, 
    IPriceRepository
{
            
    public PriceRepository(AppDbContext dbContext, IMapper<App.DAL.DTO.Price, App.Domain.Price> mapper) 
        : base(dbContext, mapper)
    {
    }
    
    public async Task<IEnumerable<App.DAL.DTO.Price>> GetAllAsync(Guid userId, bool noTracking = true)
    {
        var query = CreateQuery(noTracking);
        query = query
            .Include(p => p.Stock);

        return (await query.ToListAsync()).Select(x=>Mapper.Map(x)!);
    }
    
    // public async Task<IEnumerable<App.DAL.DTO.Portfolio>> GetAllByNameAsync(string name, bool noTracking = true)
    // {
    //     var query = CreateQuery(noTracking);
    //     return (await query.Where(a => a.Name.ToUpper().Contains(name.ToUpper())).ToListAsync())
    //         .Select(x => Mapper.Map(x)!);
    // }
    
    
}
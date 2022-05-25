using App.Contracts.DAL;
using App.Public.DTO;
using App.Public.DTO.v1;
using AutoMapper;
using Base.Contracts.Base;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class PortfolioRepository : BaseEntityRepository<App.DAL.DTO.Portfolio, App.Domain.Portfolio, AppDbContext>, 
    IPortfolioRepository
{
            
    public PortfolioRepository(AppDbContext dbContext, IMapper<App.DAL.DTO.Portfolio, App.Domain.Portfolio> mapper) 
        : base(dbContext, mapper)
    {
    }
    
    public async Task<IEnumerable<App.DAL.DTO.Portfolio>> GetAllAsync(Guid userId, bool noTracking = true)
    {
        var query = CreateQuery(noTracking);
        query = query
            .Include(p => p.AppUser)
            .Include(p => p.Stocks)
            .Include(p => p.Loans)
            .Include(p => p.Stocks)
            .Where(p => p.AppUserId == userId);

        return (await query.ToListAsync()).Select(x=>Mapper.Map(x)!);
    }
    
    public async Task<IEnumerable<App.DAL.DTO.Portfolio>> GetAllByNameAsync(string name, bool noTracking = true)
    {
        var query = CreateQuery(noTracking);
        return (await query.Where(a => a.Name.ToUpper().Contains(name.ToUpper())).ToListAsync())
            .Select(x => Mapper.Map(x)!);
    }
    
    
}
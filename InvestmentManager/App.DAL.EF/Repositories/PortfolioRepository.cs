using App.Contracts.DAL;
using App.Domain;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class PortfolioRepository : BaseEntityRepository<Portfolio, AppDbContext>, IPortfolioRepository
{
    public PortfolioRepository(AppDbContext dbContext) : base(dbContext)
    {
    }
    
    public async Task<IEnumerable<Portfolio>> GetAllByNameAsync(string name, bool noTracking = true)
    {
        var query = CreateQuery(noTracking);
        return await query.Where(a => a.Name.ToUpper().Contains(name.ToUpper())).ToListAsync();
    }
    
    public async Task<IEnumerable<Portfolio>> GetAllAsync(Guid userId, bool noTracking = true)
    {
        var query = CreateQuery(noTracking);
        query = query
            .Include(u => u.AppUser)
            .Where(m => m.AppUserId == userId);

        return await query.ToListAsync();
    }
}
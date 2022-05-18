using App.Contracts.DAL;
using App.DAL.EF.Mappers;
using App.Domain;
using Base.Contracts.Base;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;
using App.DAL.DTO;
using Loan = App.DAL.DTO.Loan;

namespace App.DAL.EF.Repositories;

public class CashRepository : BaseEntityRepository<App.DAL.DTO.Cash, App.Domain.Cash, AppDbContext>, ICashRepository
{
    public CashRepository(AppDbContext dbContext, IMapper<App.DAL.DTO.Cash, App.Domain.Cash> mapper) : base(dbContext, mapper)
    {
    }

    public async Task<IEnumerable<App.DAL.DTO.Cash>> GetAllAsync(Guid userId, bool noTracking = true)
    {
        var query = CreateQuery(noTracking);
        query = query
            .Include(c => c.Portfolio)
            .Where(c => c.Portfolio.AppUserId == userId);
        
        return (await query.ToListAsync()).Select(x=>Mapper.Map(x)!);
        
        
    }
    
}
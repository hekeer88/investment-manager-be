using App.Contracts.DAL;
using Base.Contracts.Base;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;


namespace App.DAL.EF.Repositories;

public class StockRepository : BaseEntityRepository<App.DAL.DTO.Stock, App.Domain.Stock, AppDbContext>, IStockRepository
{
    public StockRepository(AppDbContext dbContext, IMapper<App.DAL.DTO.Stock, App.Domain.Stock> mapper) : base(dbContext, mapper)
    {
    }


    public async Task<IEnumerable<App.DAL.DTO.Stock>> GetAllAsync(Guid userId, bool noTracking = true)
    {
        var query = CreateQuery(noTracking);
        query = query
            .Include(s => s.Portfolio)
            .Include(s => s.Industry)
            .Include(s => s.Region)
            .Include(s => s.Transactions)
            .Where(s => s.Portfolio.AppUserId == userId);

        return (await query.ToListAsync()).Select(x=>Mapper.Map(x)!);
    }
    
}
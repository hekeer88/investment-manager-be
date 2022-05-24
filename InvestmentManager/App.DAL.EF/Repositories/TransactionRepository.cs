using App.Contracts.DAL;
using App.Public.DTO;
using App.Public.DTO.v1;
using AutoMapper;
using Base.Contracts.Base;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class TransactionRepository : BaseEntityRepository<App.DAL.DTO.Transaction, App.Domain.Transaction, AppDbContext>, 
    ITransactionRepository
{
            
    public TransactionRepository(AppDbContext dbContext, IMapper<App.DAL.DTO.Transaction, App.Domain.Transaction> mapper) 
        : base(dbContext, mapper)
    {
    }
    
    public async Task<IEnumerable<App.DAL.DTO.Transaction>> GetAllAsync(Guid userId, bool noTracking = true)
    {
        var query = CreateQuery(noTracking);
        query = query
            .Include(t => t.Stock);
            

        return (await query.ToListAsync()).Select(x=>Mapper.Map(x)!);
    }
    
    // public async Task<IEnumerable<App.DAL.DTO.Portfolio>> GetAllByNameAsync(string name, bool noTracking = true)
    // {
    //     var query = CreateQuery(noTracking);
    //     return (await query.Where(a => a.Name.ToUpper().Contains(name.ToUpper())).ToListAsync())
    //         .Select(x => Mapper.Map(x)!);
    // }
    //
    //
}
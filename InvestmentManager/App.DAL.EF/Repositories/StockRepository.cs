using App.Contracts.DAL;
using App.Domain;
using Base.DAL.EF;

namespace App.DAL.EF.Repositories;

public class StockRepository : BaseEntityRepository<Stock, AppDbContext>, IStockRepository
{
    public StockRepository(AppDbContext dbContext) : base(dbContext)
    {
    }
}
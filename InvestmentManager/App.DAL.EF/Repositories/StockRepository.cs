using App.Contracts.DAL;
using App.DAL.EF.Mappers;
using App.Domain;
using Base.Contracts.Base;
using Base.DAL.EF;

namespace App.DAL.EF.Repositories;

public class StockRepository : BaseEntityRepository<App.DAL.DTO.Stock, App.Domain.Stock, AppDbContext>, IStockRepository
{
    public StockRepository(AppDbContext dbContext, IMapper<App.DAL.DTO.Stock, App.Domain.Stock> mapper) : base(dbContext, mapper)
    {
    }
}
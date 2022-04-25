using Base.Contracts.DAL;

namespace App.Contracts.DAL;

public interface IStockRepository : IEntityRepository<App.DAL.DTO.Stock>
{
    // custom stuff here
}
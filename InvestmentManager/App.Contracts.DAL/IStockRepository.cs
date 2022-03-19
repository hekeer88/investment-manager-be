using App.Domain;
using Base.Contracts.DAL;

namespace App.Contracts.DAL;

public interface IStockRepository : IEntityRepository<Stock>
{
    // custom stuff here
}
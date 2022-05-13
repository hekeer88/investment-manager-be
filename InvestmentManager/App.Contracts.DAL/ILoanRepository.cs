using Base.Contracts.DAL;

namespace App.Contracts.DAL;

public interface ILoanRepository : IEntityRepository<App.DAL.DTO.Loan>,
    ILoanCustom<App.DAL.DTO.Loan>
{
}

public interface ILoanCustom<TEntity>
{
    Task<IEnumerable<TEntity>> GetAllAsync(Guid portfolioId, bool noTracking = true);
    
}
using Base.Contracts.DAL;

namespace App.Contracts.DAL;

public interface ILoanRepository : IEntityRepository<App.DAL.DTO.Loan>
{
    // custom stuff here
}
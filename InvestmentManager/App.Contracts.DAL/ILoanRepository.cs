using App.Domain;
using Base.Contracts.DAL;

namespace App.Contracts.DAL;

public interface ILoanRepository : IEntityRepository<Loan>
{
    // custom stuff here
}
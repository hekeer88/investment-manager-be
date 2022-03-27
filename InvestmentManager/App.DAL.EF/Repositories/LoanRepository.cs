using App.Contracts.DAL;
using App.Domain;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class LoanRepository : BaseEntityRepository<Loan, AppDbContext>, ILoanRepository
{
    public LoanRepository(AppDbContext dbContext) : base(dbContext)
    {
    }
}
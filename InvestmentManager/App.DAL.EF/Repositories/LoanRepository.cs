using App.Contracts.DAL;
using App.DAL.EF.Mappers;
using App.Domain;
using Base.Contracts.Base;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;
using App.DAL.DTO;

namespace App.DAL.EF.Repositories;

public class LoanRepository : BaseEntityRepository<App.DAL.DTO.Loan, App.Domain.Loan, AppDbContext>, ILoanRepository
{
    public LoanRepository(AppDbContext dbContext, IMapper<App.DAL.DTO.Loan, App.Domain.Loan> mapper) : base(dbContext, mapper)
    {
    }
}
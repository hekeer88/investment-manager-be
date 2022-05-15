using App.Contracts.DAL;
using App.DAL.EF.Mappers;
using App.Domain;
using Base.Contracts.Base;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;
using App.DAL.DTO;
using Loan = App.DAL.DTO.Loan;

namespace App.DAL.EF.Repositories;

public class LoanRepository : BaseEntityRepository<App.DAL.DTO.Loan, App.Domain.Loan, AppDbContext>, ILoanRepository
{
    public LoanRepository(AppDbContext dbContext, IMapper<App.DAL.DTO.Loan, App.Domain.Loan> mapper) : base(dbContext, mapper)
    {
    }

    public async Task<IEnumerable<App.DAL.DTO.Loan>> GetAllAsync(Guid portfolioId, bool noTracking = true)
    {
        var query = CreateQuery(noTracking);
        query = query
            .Where(l => l.PortfolioId == portfolioId);
        
        return (await query.ToListAsync()).Select(x=>Mapper.Map(x)!);
        
        
    }
    
}
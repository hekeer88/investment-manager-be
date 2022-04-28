using App.BLL.DTO;
using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using App.DAL.EF.Mappers;
using Base.BLL;
using Base.Contracts.Base;

namespace App.BLL.Services;

public class LoanService: BaseEntityService<App.BLL.DTO.Loan, App.DAL.DTO.Loan, ILoanRepository>, 
    ILoanService
{
    public LoanService(ILoanRepository repository, IMapper<Loan, DAL.DTO.Loan> mapper) : base(repository, mapper)
    {
    }
}
using App.BLL.DTO;
using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using Base.BLL;
using Base.Contracts.Base;

namespace App.BLL.Services;


public class LoanService: BaseEntityService<App.Public.DTO.v1.Loan, App.BLL.DTO.Loan, App.DAL.DTO.Loan, ILoanRepository>, 
    ILoanService
{
    public LoanService(ILoanRepository repository, IMapper<Loan, DAL.DTO.Loan> bllMapper,
        IMapper<App.Public.DTO.v1.Loan, Loan> publicMapper) : base(repository, bllMapper, publicMapper)
    {
    }

    public async Task<IEnumerable<Loan>> GetAllAsync(Guid portfolioId, bool noTracking = true)
    {
        var res =
            (await Repository.GetAllAsync(portfolioId, noTracking)).Select(x => BLLMapper.Map(x)!).ToList();

        foreach (var loan in res)
        {
            loan.LoanName = loan.LoanName.ToUpper();
        }

        return res;

    }
}
using App.Contracts.DAL;
using Base.Contracts.BLL;

namespace App.Contracts.BLL.Services;

public interface ILoanService :  IEntityService<App.Public.DTO.v1.Loan, App.BLL.DTO.Loan>,
    ILoanCustom<App.BLL.DTO.Loan>
{
    
}
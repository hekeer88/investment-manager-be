using App.Contracts.DAL;
using App.Public.DTO.v1;
using Base.Contracts.BLL;

namespace App.Contracts.BLL.Services;

public interface ILoanService :  IEntityService<App.Public.DTO.v1.Loan, App.BLL.DTO.Loan>,
    ILoanCustom<App.BLL.DTO.Loan>
{
    Task<IEnumerable<App.Public.DTO.v1.Loan>> GetAllPublicAsync(Guid portfolioId,  bool noTracking = true);
    
    Task<App.Public.DTO.v1.Loan?> FirstOrDefaultPublicAsync(Guid id, bool noTracking = true);
}
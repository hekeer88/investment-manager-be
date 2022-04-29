using App.BLL.DTO;
using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using App.DAL.EF.Mappers;
using Base.BLL;
using Base.Contracts.Base;

namespace App.BLL.Services;

public class PortfolioService: BaseEntityService<App.BLL.DTO.Portfolio, App.DAL.DTO.Portfolio, IPortfolioRepository>, 
    IPortfolioService
{
    public PortfolioService(IPortfolioRepository repository, IMapper<Portfolio, DAL.DTO.Portfolio> mapper) : base(repository, mapper)
    {
    }
    
    public async Task<IEnumerable<Portfolio>> GetAllByNameAsync(string name, bool noTracking = true)
    {
        return (await Repository.GetAllByNameAsync(name, noTracking)).Select(x => Mapper.Map(x)!);
    }

    public async Task<IEnumerable<Portfolio>> GetAllAsync(Guid userId, bool noTracking = true)
    {
        
        // business logic siin, nt nagu see n2ide mis teeb portfoli nime suureks
        
        var res =
        (await Repository.GetAllAsync(userId, noTracking)).Select(x => Mapper.Map(x)!).ToList();

        foreach (var portfolio in res)
        {
            portfolio.Name = portfolio.Name.ToUpper();
        }

        return res;
    }
}
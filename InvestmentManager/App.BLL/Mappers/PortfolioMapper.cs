using AutoMapper;
using Base.DAL;

namespace App.BLL.Mappers;

public class PortfolioMapper : BaseMapper<App.BLL.DTO.Portfolio, App.DAL.DTO.Portfolio>
{
    public PortfolioMapper(IMapper mapper) : base(mapper)
    {
    }
}

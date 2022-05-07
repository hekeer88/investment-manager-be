

using App.Public.DTO.v1;
using AutoMapper;
using Base.Contracts.Base;
using Base.DAL;

namespace App.Public.DTO.Mappers;

public class PortfolioMapper : BaseMapper<App.Public.DTO.v1.Portfolio, App.BLL.DTO.Portfolio>
{
    public PortfolioMapper(IMapper mapper) : base(mapper)
    {
    }
}

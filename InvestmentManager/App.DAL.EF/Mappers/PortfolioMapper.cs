using App.DAL.DTO;
using AutoMapper;
using Base.Contracts.Base;
using Base.DAL;

namespace App.DAL.EF.Mappers;

public class PortfolioMapper : BaseMapper<Portfolio, Domain.Portfolio>
{
    public PortfolioMapper(IMapper mapper) : base(mapper)
    {
    }
}
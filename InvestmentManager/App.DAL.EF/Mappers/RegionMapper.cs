using App.DAL.DTO.Identity;
using App.Domain;
using AutoMapper;
using Base.Contracts.Base;
using Base.DAL;
using Loan = App.DAL.DTO.Loan;

namespace App.DAL.EF.Mappers;

public class RegionMapper : BaseMapper<App.DAL.DTO.Region, App.Domain.Region>
{
    public RegionMapper(IMapper mapper) : base(mapper)
    {
    }
}
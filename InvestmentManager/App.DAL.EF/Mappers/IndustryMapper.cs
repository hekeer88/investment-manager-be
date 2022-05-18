using App.DAL.DTO.Identity;
using App.Domain;
using AutoMapper;
using Base.Contracts.Base;
using Base.DAL;
using Loan = App.DAL.DTO.Loan;

namespace App.DAL.EF.Mappers;

public class IndustryMapper : BaseMapper<App.DAL.DTO.Industry, App.Domain.Industry>
{
    public IndustryMapper(IMapper mapper) : base(mapper)
    {
    }
}
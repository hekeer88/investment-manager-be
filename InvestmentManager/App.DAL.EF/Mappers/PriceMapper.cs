using App.DAL.DTO.Identity;
using App.Domain;
using AutoMapper;
using Base.Contracts.Base;
using Base.DAL;
using Loan = App.DAL.DTO.Loan;

namespace App.DAL.EF.Mappers;

public class PriceMapper : BaseMapper<App.DAL.DTO.Price, App.Domain.Price>
{
    public PriceMapper(IMapper mapper) : base(mapper)
    {
    }
}
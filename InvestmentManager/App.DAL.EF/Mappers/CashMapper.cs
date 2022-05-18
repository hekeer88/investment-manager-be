using App.DAL.DTO.Identity;
using App.Domain;
using AutoMapper;
using Base.Contracts.Base;
using Base.DAL;
using Loan = App.DAL.DTO.Loan;

namespace App.DAL.EF.Mappers;

public class CashMapper : BaseMapper<App.DAL.DTO.Cash, App.Domain.Cash>
{
    public CashMapper(IMapper mapper) : base(mapper)
    {
    }
}
using App.DAL.DTO.Identity;
using App.Domain;
using AutoMapper;
using Base.Contracts.Base;
using Base.DAL;
using Loan = App.DAL.DTO.Loan;

namespace App.DAL.EF.Mappers;

public class LoanMapper : BaseMapper<Loan, App.Domain.Loan>
{
    public LoanMapper(IMapper mapper) : base(mapper)
    {
    }
}
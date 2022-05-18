using App.DAL.DTO.Identity;
using App.Domain;
using AutoMapper;
using Base.Contracts.Base;
using Base.DAL;
using Loan = App.DAL.DTO.Loan;

namespace App.DAL.EF.Mappers;

public class TransactionMapper : BaseMapper<App.DAL.DTO.Transaction, App.Domain.Transaction>
{
    public TransactionMapper(IMapper mapper) : base(mapper)
    {
    }
}
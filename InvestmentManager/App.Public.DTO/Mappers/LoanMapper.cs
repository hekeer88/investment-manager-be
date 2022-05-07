using AutoMapper;
using Base.DAL;

namespace App.Public.DTO.Mappers;

public class LoanMapper : BaseMapper<App.Public.DTO.v1.Loan, App.BLL.DTO.Loan>
{
    public LoanMapper(IMapper mapper) : base(mapper)
    {
    }
}
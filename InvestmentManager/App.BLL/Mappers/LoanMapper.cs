using AutoMapper;
using Base.DAL;

namespace App.BLL.Mappers;

public class LoanMapper : BaseMapper<App.BLL.DTO.Loan, App.DAL.DTO.Loan>
{
    public LoanMapper(IMapper mapper) : base(mapper)
    {
    }
}
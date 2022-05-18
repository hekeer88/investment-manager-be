using AutoMapper;
using Base.DAL;

namespace App.BLL.Mappers;

public class TransactionMapper : BaseMapper<App.BLL.DTO.Transaction, App.DAL.DTO.Transaction>
{
    public TransactionMapper(IMapper mapper) : base(mapper)
    {
    }
}
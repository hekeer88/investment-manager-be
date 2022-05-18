using AutoMapper;
using Base.DAL;

namespace App.Public.DTO.Mappers;

public class TransactionMapper : BaseMapper<App.Public.DTO.v1.Transaction, App.BLL.DTO.Transaction>
{
    public TransactionMapper(IMapper mapper) : base(mapper)
    {
    }
}
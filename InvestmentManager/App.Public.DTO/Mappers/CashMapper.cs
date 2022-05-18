using AutoMapper;
using Base.DAL;

namespace App.Public.DTO.Mappers;

public class CashMapper : BaseMapper<App.Public.DTO.v1.Cash, App.BLL.DTO.Cash>
{
    public CashMapper(IMapper mapper) : base(mapper)
    {
    }
}
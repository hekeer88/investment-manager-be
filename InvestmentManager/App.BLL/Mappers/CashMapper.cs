using AutoMapper;
using Base.DAL;

namespace App.BLL.Mappers;

public class CashMapper : BaseMapper<App.BLL.DTO.Cash, App.DAL.DTO.Cash>
{
    public CashMapper(IMapper mapper) : base(mapper)
    {
    }
}
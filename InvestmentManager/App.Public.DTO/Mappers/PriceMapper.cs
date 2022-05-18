using AutoMapper;
using Base.DAL;

namespace App.Public.DTO.Mappers;

public class PriceMapper : BaseMapper<App.Public.DTO.v1.Price, App.BLL.DTO.Price>
{
    public PriceMapper(IMapper mapper) : base(mapper)
    {
    }
}
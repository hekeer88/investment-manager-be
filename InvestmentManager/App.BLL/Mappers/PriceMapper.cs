using AutoMapper;
using Base.DAL;

namespace App.BLL.Mappers;

public class PriceMapper : BaseMapper<App.BLL.DTO.Price, App.DAL.DTO.Price>
{
    public PriceMapper(IMapper mapper) : base(mapper)
    {
    }
}
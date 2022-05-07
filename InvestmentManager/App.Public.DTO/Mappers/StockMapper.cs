using AutoMapper;
using Base.DAL;

namespace App.Public.DTO.Mappers;

public class StockMapper : BaseMapper<App.Public.DTO.v1.Stock, App.BLL.DTO.Stock>
{
    public StockMapper(IMapper mapper) : base(mapper)
    {
    }
}
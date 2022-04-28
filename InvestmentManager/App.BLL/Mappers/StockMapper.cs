using AutoMapper;
using Base.DAL;

namespace App.BLL.Mappers;

public class StockMapper : BaseMapper<App.BLL.DTO.Stock, App.DAL.DTO.Stock>
{
    public StockMapper(IMapper mapper) : base(mapper)
    {
    }
}
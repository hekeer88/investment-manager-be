using App.DAL.DTO;
using AutoMapper;
using Base.Contracts.Base;
using Base.DAL;

namespace App.DAL.EF.Mappers;

public class StockMapper : BaseMapper<Stock, Domain.Stock>
{
    public StockMapper(IMapper mapper) : base(mapper)
    {
    }
}
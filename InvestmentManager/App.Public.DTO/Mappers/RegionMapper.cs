using AutoMapper;
using Base.DAL;

namespace App.Public.DTO.v1.Mappers;

public class RegionMapper : BaseMapper<App.Public.DTO.v1.Region, App.BLL.DTO.Region>
{
    public RegionMapper(IMapper mapper) : base(mapper)
    {
    }
}
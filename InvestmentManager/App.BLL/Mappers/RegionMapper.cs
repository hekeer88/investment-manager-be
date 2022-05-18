using AutoMapper;
using Base.DAL;

namespace App.BLL.Mappers;

public class RegionMapper : BaseMapper<App.BLL.DTO.Region, App.DAL.DTO.Region>
{
    public RegionMapper(IMapper mapper) : base(mapper)
    {
    }
}
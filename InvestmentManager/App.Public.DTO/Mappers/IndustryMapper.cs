using AutoMapper;
using Base.DAL;

namespace App.Public.DTO.Mappers;

public class IndustryMapper : BaseMapper<App.Public.DTO.v1.Industry, App.BLL.DTO.Industry>
{
    public IndustryMapper(IMapper mapper) : base(mapper)
    {
    }
}
using AutoMapper;
using Base.DAL;

namespace App.BLL.Mappers;

public class IndustryMapper : BaseMapper<App.BLL.DTO.Industry, App.DAL.DTO.Industry>
{
    public IndustryMapper(IMapper mapper) : base(mapper)
    {
    }
}
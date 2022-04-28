using App.DAL.DTO;
using App.DAL.DTO.Identity;
using AutoMapper;

namespace App.BLL;

public class AutomapperConfig : Profile
{
    public AutomapperConfig()
    {
        CreateMap<App.BLL.DTO.Portfolio, App.DAL.DTO.Portfolio>().ReverseMap();
        CreateMap<App.BLL.DTO.Stock, App.DAL.DTO.Stock>().ReverseMap();
        CreateMap<App.BLL.DTO.Loan, App.DAL.DTO.Loan>().ReverseMap();
        CreateMap<App.BLL.DTO.Identity.AppUser, App.DAL.DTO.Identity.AppUser>().ReverseMap();
    }
}

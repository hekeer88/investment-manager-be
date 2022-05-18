using App.DAL.DTO;
using App.DAL.DTO.Identity;
using AutoMapper;

namespace App.BLL;

public class AutomapperConfig : Profile
{
    public AutomapperConfig()
    {
        CreateMap<App.BLL.DTO.Identity.AppUser, App.DAL.DTO.Identity.AppUser>().ReverseMap();
        
        CreateMap<App.BLL.DTO.Portfolio, App.DAL.DTO.Portfolio>().ReverseMap();
        CreateMap<App.BLL.DTO.Stock, App.DAL.DTO.Stock>().ReverseMap();
        CreateMap<App.BLL.DTO.Loan, App.DAL.DTO.Loan>().ReverseMap();
        CreateMap<App.BLL.DTO.Cash, App.DAL.DTO.Cash>().ReverseMap();
        CreateMap<App.BLL.DTO.Industry, App.DAL.DTO.Industry>().ReverseMap();
        CreateMap<App.BLL.DTO.Price, App.DAL.DTO.Price>().ReverseMap();
        CreateMap<App.BLL.DTO.Region, App.DAL.DTO.Region>().ReverseMap();
        CreateMap<App.BLL.DTO.Transaction, App.DAL.DTO.Transaction>().ReverseMap();
       
        
        CreateMap<App.Public.DTO.v1.Portfolio, App.BLL.DTO.Portfolio>().ReverseMap();
        CreateMap<App.Public.DTO.v1.Stock, App.BLL.DTO.Stock>().ReverseMap();
        CreateMap<App.Public.DTO.v1.Loan, App.BLL.DTO.Loan>().ReverseMap();
        
        CreateMap<App.Public.DTO.v1.Cash, App.BLL.DTO.Cash>().ReverseMap();
        CreateMap<App.Public.DTO.v1.Industry, App.BLL.DTO.Industry>().ReverseMap();
        CreateMap<App.Public.DTO.v1.Price, App.BLL.DTO.Price>().ReverseMap();
        CreateMap<App.Public.DTO.v1.Region, App.BLL.DTO.Region>().ReverseMap();
        CreateMap<App.Public.DTO.v1.Transaction, App.BLL.DTO.Transaction>().ReverseMap();
        // CreateMap<App.Public.DTO.v1.Identity.AppUser, App.BLL.DTO.Identity.AppUser>().ReverseMap();
    }
}

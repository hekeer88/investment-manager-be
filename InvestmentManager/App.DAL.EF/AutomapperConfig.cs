using App.DAL.DTO;
using App.DAL.DTO.Identity;
using AutoMapper;

namespace App.DAL.EF;

public class AutomapperConfig : Profile
{
    public AutomapperConfig()
    {
        CreateMap<Portfolio, App.Domain.Portfolio>().ReverseMap();
        CreateMap<Stock, App.Domain.Stock>().ReverseMap();
        CreateMap<Loan, App.Domain.Loan>().ReverseMap();
        CreateMap<AppUser, App.Domain.identity.AppUser>().ReverseMap();
    }
}

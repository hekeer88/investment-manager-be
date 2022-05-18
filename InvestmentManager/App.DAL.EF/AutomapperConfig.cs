using App.DAL.DTO;
using App.DAL.DTO.Identity;
using AutoMapper;

namespace App.DAL.EF;

public class AutomapperConfig : Profile
{
    public AutomapperConfig()
    {
        CreateMap<AppUser, App.Domain.identity.AppUser>().ReverseMap();
        
        CreateMap<Portfolio, App.Domain.Portfolio>().ReverseMap();
        CreateMap<Stock, App.Domain.Stock>().ReverseMap();
        CreateMap<Loan, App.Domain.Loan>().ReverseMap();
        CreateMap<Cash, App.Domain.Cash>().ReverseMap();
        CreateMap<Industry, App.Domain.Industry>().ReverseMap();
        CreateMap<Price, App.Domain.Price>().ReverseMap();
        CreateMap<Region, App.Domain.Region>().ReverseMap();
        CreateMap<Transaction, App.Domain.Transaction>().ReverseMap();

    }
}

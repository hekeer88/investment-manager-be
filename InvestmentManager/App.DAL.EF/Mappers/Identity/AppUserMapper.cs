using App.DAL.DTO.Identity;
using AutoMapper;
using Base.DAL;

namespace App.DAL.EF.Mappers.Identity;

public class AppUserMapper : BaseMapper<AppUser, App.Domain.identity.AppUser>
{
    public AppUserMapper(IMapper mapper) : base(mapper)
    {
    }
}
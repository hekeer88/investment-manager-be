using Base.Domain;

namespace WebApp.DTO;

public class RegionDTO : DomainEntityMetaId
{
    public string Country { get; set; } = default!;
    public string Continent { get; set; } = default!;
}
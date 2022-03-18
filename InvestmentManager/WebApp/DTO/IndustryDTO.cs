using Base.Domain;

namespace WebApp.DTO;

public class IndustryDTO : DomainEntityMetaId
{
    public string Name { get; set; } = default!;
}
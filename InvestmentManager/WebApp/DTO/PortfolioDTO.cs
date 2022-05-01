using Base.Domain;

namespace WebApp.DTO;

public class PortfolioDTO : DomainEntityMetaId
{
    public string Name { get; set; } = default!;
}
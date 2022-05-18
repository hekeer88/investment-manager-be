using System.ComponentModel.DataAnnotations;
using App.Resources.App.Domain;
using Base.Domain;

namespace App.Public.DTO.v1;

public class Cash : DomainEntityId
{
    public string Currency { get; set; } = default!;
    public Guid PortfolioId { get; set; }
    // public Portfolio? Portfolio { get; set; }

}
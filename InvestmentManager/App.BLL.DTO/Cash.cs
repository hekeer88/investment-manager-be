using System.ComponentModel.DataAnnotations;
using App.Resources.App.Domain;
using Base.Domain;

namespace App.BLL.DTO;

public class Cash : DomainEntityId
{
    public string Currency { get; set; } = default!;
    public ICollection<Transaction>? Transactions { get; set; }

    public Guid PortfolioId { get; set; }
    public Portfolio? Portfolio { get; set; }

}
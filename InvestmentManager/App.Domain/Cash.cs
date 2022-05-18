using System.ComponentModel.DataAnnotations;
using System.Transactions;
using Base.Domain;

namespace App.Domain;

public class Cash : DomainEntityMetaId
{

    public string Currency { get; set; } = default!;
    public ICollection<Transaction>? Transactions { get; set; }

    public Guid PortfolioId { get; set; }
    public Portfolio? Portfolio { get; set; }


}
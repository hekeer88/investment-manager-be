using System.ComponentModel.DataAnnotations;
using System.Transactions;
using Base.Domain;

namespace App.Domain;

public class Cash : DomainEntityMetaId
{
    [Display(ResourceType = typeof(App.Resources.App.Domain.Cash), Name = nameof(Currency))]
    public string Currency { get; set; } = default!;
    public ICollection<Transaction>? Transactions { get; set; }

    public Guid PortfolioId { get; set; }
    [Display(ResourceType = typeof(App.Resources.App.Domain.Cash), Name = nameof(Portfolio))]
    public Portfolio? Portfolio { get; set; }


}
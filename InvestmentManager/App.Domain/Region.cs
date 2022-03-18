using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Transactions;
using App.Domain.identity;
using Base.Domain;

namespace App.Domain;

public class Region : DomainEntityMetaId
{

    [Display(ResourceType = typeof(App.Resources.App.Domain.Region), Name = nameof(Country))]
    [Column(TypeName = "jsonb")]
    public LangStr Country { get; set; } = new();

    [Column(TypeName = "jsonb")]
    [Display(ResourceType = typeof(App.Resources.App.Domain.Region), Name = nameof(Continent))]
    public LangStr Continent { get; set; } = new();
    
    public ICollection<Stock>? Stocks { get; set; }
    public ICollection<Loan>? Loans { get; set; }
    
}
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Transactions;
using App.Domain.identity;
using Base.Domain;

namespace App.Domain;

public class Region : DomainEntityMetaId
{
    [MaxLength(32)]
    [Column(TypeName = "jsonb")]
    public LangStr Country { get; set; } = new();
    [MaxLength(32)]
    [Column(TypeName = "jsonb")]
    public LangStr Continent { get; set; } = new();
    
    public ICollection<Stock>? Stocks { get; set; }
    public ICollection<Loan>? Loans { get; set; }
    
}
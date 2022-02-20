using System.ComponentModel.DataAnnotations;
using System.Transactions;
using App.Domain.identity;
using Base.Domain;

namespace App.Domain;

public class Region : DomainEntityMetaId
{
    [MaxLength(32)]
    public string Country { get; set; } = default!;
    [MaxLength(32)]
    public string Continent { get; set; } = default!;
    
    public ICollection<Stock>? Stocks { get; set; }
    public ICollection<Loan>? Loans { get; set; }
    
}
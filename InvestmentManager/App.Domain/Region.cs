using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Transactions;
using App.Domain.identity;
using Base.Domain;

namespace App.Domain;

public class Region : DomainEntityMetaId
{
    
    public string Country { get; set; } = default!;
    public string Continent { get; set; } = default!;
    
    public ICollection<Stock>? Stocks { get; set; }
    public ICollection<Loan>? Loans { get; set; }
    
}
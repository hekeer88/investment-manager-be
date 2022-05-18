
using Base.Domain;

namespace App.Public.DTO.v1;

public class Region : DomainEntityMetaId
{
    
    public string Country { get; set; } = default!;
    public string Continent { get; set; } = default!;
    
    public ICollection<Stock>? Stocks { get; set; }
    public ICollection<Loan>? Loans { get; set; }
    
}
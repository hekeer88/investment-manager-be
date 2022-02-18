using System.ComponentModel.DataAnnotations;
using App.Domain.identity;
using Base.Domain;

namespace App.Domain;

public class Portfolio : DomainEntityMetaId
{
    [MaxLength(64)]
    public string Name { get; set; } = default!;
    [MaxLength(512)]
    public string? Description { get; set; } 
    
    public ICollection<Stock>? Stocks { get; set; }
    public ICollection<Loan>? Loans { get; set; }
    public ICollection<Cash>? Cashes { get; set; }

    public Guid AppUserId { get; set; }
    public AppUser? AppUser { get; set; }
    
    
}
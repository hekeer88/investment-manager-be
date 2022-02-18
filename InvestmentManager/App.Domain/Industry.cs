using System.ComponentModel.DataAnnotations;
using System.Transactions;
using App.Domain.identity;
using Base.Domain;

namespace App.Domain;

public class Industry : DomainEntityMetaId
{
    [MaxLength(32)]
    public string Name { get; set; } = default!;
    public ICollection<Stock>? Stocks { get; set; }
    
    // no appuser if for standard and shared Industry
    public Guid? AppUserId { get; set; }
    public AppUser? AppUser { get; set; }

}
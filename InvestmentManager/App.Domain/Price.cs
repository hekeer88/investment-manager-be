using System.ComponentModel.DataAnnotations;
using System.Transactions;
using Base.Domain;

namespace App.Domain;

public class Industry : DomainEntityMetaId
{
    [MaxLength(32)]
    public string Name { get; set; } = default!;
    public ICollection<Stock>? Stocks { get; set; }

}
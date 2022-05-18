using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Transactions;
using App.Domain.identity;
using Base.Domain;

namespace App.Domain;

public class Industry : DomainEntityMetaId
{

    public string Name { get; set; } = default!;
    public ICollection<Stock>? Stocks { get; set; }
}
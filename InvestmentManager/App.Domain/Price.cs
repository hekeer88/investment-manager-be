using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Transactions;
using Base.Domain;

namespace App.Domain;

public class Price : DomainEntityMetaId
{
    [Range(0, 9999999999.99)]
    public decimal CurrentPrice { get; set; } = default!;
    public DateTime PriceTime { get; set; } = default;
    
    //TODO: tagasiviide?
    public Guid StockId { get; set; }
    public Stock? Stock { get; set; }

    // public override string ToString()
    // {
    //     return CurrentPrice.ToString(CultureInfo.InvariantCulture);
    // }
}
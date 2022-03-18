using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Transactions;
using Base.Domain;

namespace App.Domain;

public class Price : DomainEntityMetaId
{
    [Range(0, 9999999999.99)]
    [Display(ResourceType = typeof(App.Resources.App.Domain.Price), Name = nameof(CurrentPrice))]
    public decimal CurrentPrice { get; set; } = default!;
    [Display(ResourceType = typeof(App.Resources.App.Domain.Price), Name = nameof(PriceTime))]
    public DateTime PriceTime { get; set; } = default;
    
    public Guid StockId { get; set; }
    [Display(ResourceType = typeof(App.Resources.App.Domain.Price), Name = nameof(Stock))]
    public Stock? Stock { get; set; }

    public override string ToString()
    {
        return CurrentPrice.ToString(CultureInfo.InvariantCulture);
    }
}
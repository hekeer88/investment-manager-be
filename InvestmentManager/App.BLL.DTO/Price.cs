using System.ComponentModel.DataAnnotations;
using App.Resources.App.Domain;
using Base.Domain;

namespace App.BLL.DTO;

public class Price : DomainEntityId
{
    [Range(0, 9999999999.99)]
    public decimal CurrentPrice { get; set; } = default!;
    public DateTime PriceTime { get; set; } = default;
    
    public Guid StockId { get; set; }
    public Stock? Stock { get; set; }

}
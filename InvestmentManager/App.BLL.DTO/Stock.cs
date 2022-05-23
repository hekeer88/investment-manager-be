using System.ComponentModel.DataAnnotations;
using App.Resources.App.Domain;
using Base.Domain;

namespace App.BLL.DTO;

public class Stock : DomainEntityId
{
    [MaxLength(32)]
    public string Company { get; set; } = default!;
    [MaxLength(8)]
    public string Ticker { get; set; } = default!;
    [MaxLength(256)]
    public string? Comment { get; set; }

    public ICollection<Price>? Prices { get; set; } = new List<Price>();
    public ICollection<Transaction>? Transactions { get; set; }
    
    public Guid? RegionId { get; set; }
    [Display(ResourceType = typeof(App.Resources.App.Domain.Stock), Name=nameof(Region))]
    public Region? Region { get; set; }
    public Guid PortfolioId { get; set; }
    [Display(ResourceType = typeof(App.Resources.App.Domain.Stock), Name=nameof(Portfolio))]
    public Portfolio? Portfolio { get; set; }
    public Guid? IndustryId { get; set; }
    [Display(ResourceType = typeof(App.Resources.App.Domain.Stock), Name=nameof(Industry))]
    public Industry? Industry { get; set; }
    
    // public virtual decimal? LoanSum
    // {
    //     get
    //     {
    //         return Prices?.OrderByDescending(p => p.PriceTime).FirstOrDefault()?.CurrentPrice;
    //     }
    // }

    // public virtual decimal? LatestPrice
    // {
    //     get;
    //     set;
    // }

    
    // public Stock()
    // {
    //     var initialPrice = new Price()
    //     {
    //         CurrentPrice = 0.0m,
    //         PriceTime = DateTime.UtcNow
    //     };
    //     Prices.Add(initialPrice);
    // }
    //
    // public Price GetLastPrice()
    // {
    //     return Prices.OrderByDescending(x => x.PriceTime).FirstOrDefault()!;
    // }
}
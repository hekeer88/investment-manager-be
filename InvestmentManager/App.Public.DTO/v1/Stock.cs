using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using App.Resources.App.Domain;
using Base.Domain;

namespace App.Public.DTO.v1;

public class Stock : DomainEntityId
{
    [MaxLength(32)]
    [Display(ResourceType = typeof(App.Resources.App.Domain.Stock), Name=nameof(Company))]
    public string Company { get; set; } = default!;
    [MaxLength(8)]
    [Display(ResourceType = typeof(App.Resources.App.Domain.Stock), Name=nameof(Ticker))]
    public string Ticker { get; set; } = default!;
    [MaxLength(256)]
    [Display(ResourceType = typeof(App.Resources.App.Domain.Stock), Name=nameof(Comment))]
    public string? Comment { get; set; }
    
    public virtual decimal? Quantity { get; set; }
    public virtual decimal? LatestPrice { get; set; }

    // public ICollection<Price> Prices { get; set; } = new List<Price>();
    // public ICollection<Transaction>? Transactions { get; set; }
    public Guid? RegionId { get; set; }
    public Region? Region { get; set; }
    public Guid PortfolioId { get; set; }
    public Portfolio? Portfolio { get; set; }
    public Guid? IndustryId { get; set; }
    public Industry? Industry { get; set; }
    
    
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
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
    
    public decimal? Balance { get; set; }

    public ICollection<Price>? Prices { get; set; }
    public ICollection<Transaction>? Transactions { get; set; }
    
    public Guid? RegionId { get; set; }
    public Region? Region { get; set; }
    public Guid PortfolioId { get; set; }
    public Portfolio? Portfolio { get; set; }
    public Guid? IndustryId { get; set; }
    public Industry? Industry { get; set; }
    
    public virtual decimal? Quantity
    {
        get
        {
            return Transactions?.Sum(t => t.Quantity) ?? 0;
        }
    }

    public virtual decimal? LatestPrice
    {
        get
        {
            return Prices?.OrderByDescending(p => p.PriceTime).FirstOrDefault()?.CurrentPrice ?? 0;
        }
    }

    
    // public Price GetLastPrice()
    // {
    //     return Prices.OrderByDescending(x => x.PriceTime).FirstOrDefault()!;
    // }
}
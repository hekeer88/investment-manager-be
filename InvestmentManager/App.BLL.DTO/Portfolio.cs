using System.ComponentModel.DataAnnotations;
using App.BLL.DTO.Identity;
using App.Resources.App.Domain;
using Base.Domain;
using Loan = App.BLL.DTO.Loan;
using Stock = App.BLL.DTO.Stock;

namespace App.BLL.DTO;

public class Portfolio : DomainEntityId
{
    [MaxLength(64)]
    [Display(ResourceType = typeof(App.Resources.App.Domain.Portfolio), Name = nameof(Name))]
    public string Name { get; set; } = default!;
    
    [MaxLength(512)]
    [Display(ResourceType = typeof(App.Resources.App.Domain.Portfolio), Name=nameof(Description))]
    public string? Description { get; set; } 
    
    public ICollection<Stock>? Stocks { get; set; }
    public ICollection<Loan>? Loans { get; set; }
    public ICollection<Cash>? Cashes { get; set; }

    // TODO: must have UserId, but for testing is turned off
    public Guid? AppUserId { get; set; }
    [Display(ResourceType = typeof(App.Resources.App.Domain.Portfolio), Name=nameof(AppUser))]
    public AppUser? AppUser { get; set; }

}
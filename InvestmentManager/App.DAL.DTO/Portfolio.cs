using System.ComponentModel.DataAnnotations;
using App.DAL.DTO.Identity;
using App.Resources.App.Domain;
using Base.Contracts.Domain;
using Base.Domain;

namespace App.DAL.DTO;

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
    // Probably dont need App user in DTO
    public Guid? AppUserId { get; set; }
    [Display(ResourceType = typeof(App.Resources.App.Domain.Portfolio), Name=nameof(AppUser))]
    public AppUser? AppUser { get; set; }

}
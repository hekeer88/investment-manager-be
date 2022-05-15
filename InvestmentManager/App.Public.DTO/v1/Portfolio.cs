using System.ComponentModel.DataAnnotations;
using App.BLL.DTO.Identity;
using App.Resources.App.Domain;
using Base.Domain;

namespace App.Public.DTO.v1;

public class Portfolio : DomainEntityId
{
    [MaxLength(64)]
    [Display(ResourceType = typeof(App.Resources.App.Domain.Portfolio), Name = nameof(Name))]
    public string Name { get; set; } = default!;
    
    [MaxLength(512)]
    [Display(ResourceType = typeof(App.Resources.App.Domain.Portfolio), Name=nameof(Description))]
    public string? Description { get; set; }
    
    public virtual decimal? LoanSum { get; set; }
    
    public Guid? AppUserId { get; set; }
}
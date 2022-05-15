﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using App.Domain.identity;
using Base.Domain;

namespace App.Domain;

public class Portfolio : DomainEntityMetaId
{
    
    [MaxLength(64)]
    [Display(ResourceType = typeof(App.Resources.App.Domain.Portfolio), Name = nameof(Name))]
    public string Name { get; set; } = default!;
    
    [MaxLength(512)]
    // TODO: can be remove for REST API, only webcontroller use it
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
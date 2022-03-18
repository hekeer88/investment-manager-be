using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Transactions;
using App.Domain.identity;
using Base.Domain;

namespace App.Domain;

public class Industry : DomainEntityMetaId
{

    [Display(ResourceType = typeof(App.Resources.App.Domain.Industry), Name = nameof(Name))]
    [Column(TypeName = "jsonb")]
    public LangStr Name { get; set; } = new();
    public ICollection<Stock>? Stocks { get; set; }
    
    // no appuser if for standard and shared Industry
    public Guid? AppUserId { get; set; }
    public AppUser? AppUser { get; set; }

}
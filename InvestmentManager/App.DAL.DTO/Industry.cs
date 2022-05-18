using System.ComponentModel.DataAnnotations;
using App.Resources.App.Domain;
using Base.Domain;

namespace App.DAL.DTO;

public class Industry : DomainEntityId
{
    public string Name { get; set; } = default!;
    public ICollection<Stock>? Stocks { get; set; }
}
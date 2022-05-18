using System.ComponentModel.DataAnnotations;
using App.Resources.App.Domain;
using Base.Domain;

namespace App.BLL.DTO;

public class Region : DomainEntityId
{
    public string Country { get; set; } = default!;
    public string Continent { get; set; } = default!;
    
    public ICollection<Stock>? Stocks { get; set; }
    public ICollection<Loan>? Loans { get; set; }
}
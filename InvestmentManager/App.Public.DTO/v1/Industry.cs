
using Base.Domain;

namespace App.Public.DTO.v1;

public class Industry : DomainEntityMetaId
{

    public string Name { get; set; } = default!;
    // public ICollection<Stock>? Stocks { get; set; }
}
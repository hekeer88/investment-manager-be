using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace App.Domain;

public class Stock : DomainEntityMetaId
{
    [MaxLength(32)]
    public string Company { get; set; } = default!;
    [MaxLength(8)]
    public string Ticker { get; set; } = default!;
    [MaxLength(256)]
    public string? Comment { get; set; }
    
}
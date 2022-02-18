﻿using System.ComponentModel.DataAnnotations;
using System.Transactions;
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
    
    public ICollection<Price>? Prices { get; set; }
    public ICollection<Transaction>? Transactions { get; set; }
    
    public Guid PortfolioId { get; set; }
    public Portfolio? Portfolio { get; set; }
    
    public Guid IndustryId { get; set; }
    public Industry? Industry { get; set; }

    
}
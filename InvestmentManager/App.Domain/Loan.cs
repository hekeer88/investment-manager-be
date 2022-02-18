using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace App.Domain;

public class Loan : DomainEntityMetaId
{
    [MaxLength(32)]
    public string? LoanName { get; set; }
    [MaxLength(64)]
    public string BorrowerName { get; set; } = default!;
    [MaxLength(32)]
    public string ContractNumber { get; set; } = default!;
    public char Collateral { get; set; } = default!;
    public DateTime LoanDate { get; set; } = default!;
    public DateTime EndDate { get; set; } = default!;
    [Range(0, 9999999999.99)]
    public decimal Amount { get; set; } = default!;
    public char ScheduleType { get; set; } = default!;
    [Range(0, 999.99)]
    public decimal Interest { get; set; } = default!;
    
    public ICollection<Transaction>? Transactions { get; set; }

    public Guid PortfolioId { get; set; }
    public Portfolio? Portfolio { get; set; }
    
    
}
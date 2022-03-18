using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace App.Domain;

public class Loan : DomainEntityMetaId
{
    [MaxLength(32)]
    [Display(ResourceType = typeof(App.Resources.App.Domain.Loan), Name = nameof(LoanName))]
    public string? LoanName { get; set; }
    
    [MaxLength(64)]
    [Display(ResourceType = typeof(App.Resources.App.Domain.Loan), Name = nameof(BorrowerName))]
    public string BorrowerName { get; set; } = default!;
    
    [MaxLength(32)]
    [Display(ResourceType = typeof(App.Resources.App.Domain.Loan), Name = nameof(ContractNumber))]
    public string ContractNumber { get; set; } = default!;
    
    [Display(ResourceType = typeof(App.Resources.App.Domain.Loan), Name = nameof(Collateral))]
    public char Collateral { get; set; } = default!;
    [Display(ResourceType = typeof(App.Resources.App.Domain.Loan), Name = nameof(LoanDate))]
    public DateTime LoanDate { get; set; } = default!;
    [Display(ResourceType = typeof(App.Resources.App.Domain.Loan), Name = nameof(EndDate))]
    public DateTime EndDate { get; set; } = default!;
    
    [Range(0, 9999999999.99)]
    [Display(ResourceType = typeof(App.Resources.App.Domain.Loan), Name = nameof(Amount))]
    public decimal Amount { get; set; } = default!;
    
    [Display(ResourceType = typeof(App.Resources.App.Domain.Loan), Name = nameof(ScheduleType))]
    public char ScheduleType { get; set; } = default!;
    
    [Range(0, 999.99)]
    [Display(ResourceType = typeof(App.Resources.App.Domain.Loan), Name = nameof(Interest))]
    public decimal Interest { get; set; } = default!;
    

    public ICollection<Transaction>? Transactions { get; set; }
    
    public Guid PortfolioId { get; set; }
    public Portfolio? Portfolio { get; set; }
    
    public Guid RegionId { get; set; }
    public Region? Region { get; set; }
    
    
}
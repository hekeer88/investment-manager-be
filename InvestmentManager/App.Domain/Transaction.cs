using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Metrics;
using System.Transactions;
using Base.Domain;

namespace App.Domain;

public class Transaction : DomainEntityMetaId
{
    [Display(ResourceType = typeof(App.Resources.App.Domain.Transaction), Name = nameof(Quantity))]
    public int? Quantity { get; set; }
    [Range(0, 9999999999.99)]
    [Display(ResourceType = typeof(App.Resources.App.Domain.Transaction), Name = nameof(TransactionPrice))]
    public decimal TransactionPrice { get; set; } = default!;
    [Display(ResourceType = typeof(App.Resources.App.Domain.Transaction), Name = nameof(TransactionDate))]
    public DateTime TransactionDate { get; set; } = default!;
    [Display(ResourceType = typeof(App.Resources.App.Domain.Transaction), Name = nameof(Type))]
    public char Type { get; set; } = default!;
    
    public Guid? StockId { get; set; }
    [Display(ResourceType = typeof(App.Resources.App.Domain.Transaction), Name = nameof(Instrument))]
    public Stock? Stock { get; set; }
    
    public Guid? LoanId { get; set; }
    [Display(ResourceType = typeof(App.Resources.App.Domain.Transaction), Name = nameof(Instrument))]
    public Loan? Loan { get; set; }
    
    public Guid? CashId { get; set; }
    [Display(ResourceType = typeof(App.Resources.App.Domain.Transaction), Name = nameof(Instrument))]
    public Cash? Cash { get; set; }


}
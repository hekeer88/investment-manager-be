using System.ComponentModel.DataAnnotations;
using System.Transactions;
using Base.Domain;

namespace App.Domain;

public class Transaction : DomainEntityMetaId
{
    public int? Quantity { get; set; }
    [Range(0, 9999999999.99)]
    public decimal TransactionPrice { get; set; } = default!;
    public DateTime TransactionDate { get; set; } = default!;
    public char Type { get; set; } = default!;
    
    public Guid? StockId { get; set; }
    public Stock? Stock { get; set; }
    
    public Guid? LoanId { get; set; }
    public Loan? Loan { get; set; }
    
    public Guid? CashId { get; set; }
    public Cash? Cash { get; set; }


}
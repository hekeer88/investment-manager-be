using System.ComponentModel.DataAnnotations;
using App.Resources.App.Domain;
using Base.Domain;

namespace App.BLL.DTO;

public class Transaction : DomainEntityId
{
    public int? Quantity { get; set; }
    [Range(0, 9999999999.99)]
    public decimal TransactionPrice { get; set; } = default!;
    public DateTime TransactionDate { get; set; } = default!;

    public Guid? StockId { get; set; }
    public Stock? Stock { get; set; }
    
    public Guid? LoanId { get; set; }
    public Loan? Loan { get; set; }
    
    public Guid? CashId { get; set; }
    public Cash? Cash { get; set; }
    
    public Domain.Transaction.ETransactionType TransactionType { get; set; }

    // investment(BUY) transactions are negative and selling(SELL) transactions are positive
    public virtual decimal Amount => Quantity * TransactionPrice * -1 ?? new decimal(0.0);
    public double YearsFromFirstTransaction;
}
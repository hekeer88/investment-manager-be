using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Metrics;
using System.Text.Json.Serialization;
using System.Transactions;
using Base.Domain;

namespace App.Public.DTO.v1;

public class Transaction : DomainEntityId
{
    public int? Quantity { get; set; }
    [Range(0, 9999999999.99)]
    public decimal TransactionPrice { get; set; } = default!;
    public DateTime TransactionDate { get; set; } = default!;

    public Guid? StockId { get; set; }
    // public Stock? Stock { get; set; }
    
    public Guid? LoanId { get; set; }
    // public Loan? Loan { get; set; }
    
    public Guid? CashId { get; set; }
    // public Cash? Cash { get; set; }
    
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public Domain.Transaction.ETransactionType TransactionType { get; set; }


}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ElTaxi.BuildingBlocks.Domain;
using ElTaxi.Domain.Enums;

namespace ElTaxi.Domain.Aggregates;

public class Payment : Entity, IAggregateRoot
{
    public decimal AmountInCents { get; set; }
    public Currency Currency { get; set; }
    public PaymentMethods Method { get; set; }
    public PaymentStatus Status { get; set; }
    public string TransactionId { get; set; } = null!;
    public DateTime PaidAt { get; set; }
    public DateTime? RefundedAt { get; set; }

    private Payment() { }

    private Payment(decimal amountInCents, Currency currency, PaymentMethods method, string transactionId)
    {
        AmountInCents = amountInCents;
        Currency = currency;
        Method = method;
        Status = PaymentStatus.Pending;
        TransactionId = transactionId;
        PaidAt = DateTime.UtcNow;
    }

    public static Payment Create(decimal amountInCents, Currency currency, PaymentMethods method, string transactionId)
    {
        return new Payment(amountInCents, currency, method, transactionId);
    }

    public void MarkAsCompleted()
    {
        Status = PaymentStatus.Completed;
    }
    public void MarkAsFailed()
    {
        Status = PaymentStatus.Failed;
    }
    public void MarkAsRefunded()
    {
        Status = PaymentStatus.Refunded;
        RefundedAt = DateTime.UtcNow;
    }
}

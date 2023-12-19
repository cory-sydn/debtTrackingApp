using System;
using System.Collections.Generic;

namespace debtTrackingApp.Models.EntityModel;

public partial class DebtRecord
{
    public int Id { get; set; }

    public int DebitorId { get; set; }

    public string? ListId { get; set; }

    public string? FileNo { get; set; }

    public int? CreditorId { get; set; }

    public int? GuarantorId { get; set; }

    public string? SalesChannel { get; set; }

    public decimal? GrossDebt { get; set; }

    public decimal? PaidAmount { get; set; }

    public decimal? UnpaidAmount { get; set; }

    public DateTime? DueDate { get; set; }

    public DateTime? PaymentDate { get; set; }

    public string? DelayDaysCount { get; set; }

    public string? PaymentStatus { get; set; }

    public string? PayorBank { get; set; }

    public string? PayorBankBranch { get; set; }

    public DateTime? InsertedDate { get; set; }

    public int? LastCallId { get; set; }

    public virtual Creditor? Creditor { get; set; }

    public virtual Debitor Debitor { get; set; } = null!;

    public virtual Guarantor? Guarantor { get; set; }

    public virtual DebtCallDetail? LastCall { get; set; }

    public virtual ICollection<DebtCallDetail> Calls { get; set; } = new List<DebtCallDetail>();
}

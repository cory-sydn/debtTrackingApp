using System;
using System.Collections.Generic;

namespace debtTrackingApp.Models.EntityModel;

public partial class DebtCallDetail
{
    public int CallId { get; set; }

    public int? CreditorId { get; set; }

    public int? Rcid { get; set; }

    public string? CallNote { get; set; }

    public DateTime? PromisedPayDate { get; set; }

    public DateTime? CallDate { get; set; }

    public int? AgentId { get; set; }

    public string? CallStatus { get; set; }

    public DateTime? InsertedDate { get; set; }

    public virtual Agent? Agent { get; set; }

    public virtual Creditor? Creditor { get; set; }

    public virtual ICollection<DebtRecord> DebtRecords { get; set; } = new List<DebtRecord>();

    public virtual ResultCode? Rc { get; set; }

    public virtual ICollection<DebtRecord> Debts { get; set; } = new List<DebtRecord>();
}

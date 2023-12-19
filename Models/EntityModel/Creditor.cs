using System;
using System.Collections.Generic;

namespace debtTrackingApp.Models.EntityModel;

public partial class Creditor
{
    public int Id { get; set; }

    public string? CreditorName { get; set; }

    public string? PayorBank { get; set; }

    public string? PayorBankBranch { get; set; }

    public DateTime? InsertedDate { get; set; }

    public virtual ICollection<DebtCallDetail> DebtCallDetails { get; set; } = new List<DebtCallDetail>();

    public virtual ICollection<DebtRecord> DebtRecords { get; set; } = new List<DebtRecord>();
}

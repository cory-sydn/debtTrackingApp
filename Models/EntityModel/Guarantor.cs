using System;
using System.Collections.Generic;

namespace debtTrackingApp.Models.EntityModel;

public partial class Guarantor
{
    public int Id { get; set; }

    public DateTime? InsertedDate { get; set; }

    public string? GuarantorName { get; set; }

    public string? GuarantorPhoneNum { get; set; }

    public string? GuarantorAddress { get; set; }

    public virtual ICollection<DebtRecord> DebtRecords { get; set; } = new List<DebtRecord>();
}

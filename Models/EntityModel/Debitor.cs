using System;
using System.Collections.Generic;

namespace debtTrackingApp.Models.EntityModel;

public partial class Debitor
{
    public int Id { get; set; }

    public DateTime? InsertedDate { get; set; }

    public string? DebitorName { get; set; }

    public string? NationalIdnumber { get; set; }

    public string? TelephoneNum { get; set; }

    public string? Address { get; set; }

    public DateTime? AppointDate { get; set; }

    public virtual ICollection<DebtRecord> DebtRecords { get; set; } = new List<DebtRecord>();
}

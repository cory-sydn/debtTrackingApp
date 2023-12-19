using System;
using System.Collections.Generic;

namespace debtTrackingApp.Models.EntityModel;

public partial class ResultCode
{
    public int Id { get; set; }

    public string? MainCategory { get; set; }

    public string? SubCategory { get; set; }

    public string? AccessStatus { get; set; }

    public string? WorkGroup { get; set; }

    public virtual ICollection<DebtCallDetail> DebtCallDetails { get; set; } = new List<DebtCallDetail>();
}

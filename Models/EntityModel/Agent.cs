using System;
using System.Collections.Generic;

namespace debtTrackingApp.Models.EntityModel;

public partial class Agent
{
    public int Id { get; set; }

    public string? AgentName { get; set; }

    public string? ProjectName { get; set; }

    public string? Password { get; set; }

    public bool? IsActive { get; set; }

    public bool? IsExecutive { get; set; }

    public virtual ICollection<DebtCallDetail> DebtCallDetails { get; set; } = new List<DebtCallDetail>();
}

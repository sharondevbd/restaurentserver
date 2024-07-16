using System;
using System.Collections.Generic;

namespace Express_Cafe_Core.Models;

public partial class Requisition
{
    public int RequisitionId { get; set; }

    public DateTime RequisitionDate { get; set; }

    public decimal RequestedQuantity { get; set; }

    public int ItemId { get; set; }

    public string RequestedBy { get; set; } = null!;

    public virtual Item Item { get; set; } = null!;
}

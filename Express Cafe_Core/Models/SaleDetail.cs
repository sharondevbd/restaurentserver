using System;
using System.Collections.Generic;

namespace Express_Cafe_Core.Models;

public partial class SaleDetail
{
    public int Id { get; set; }

    public int SaleHeaderId { get; set; }

    public int DailyMenuId { get; set; }

    public int Quantity { get; set; }

    public virtual DailyMenu DailyMenu { get; set; } = null!;

    public virtual SaleHeader SaleHeader { get; set; } = null!;
}

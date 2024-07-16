using System;
using System.Collections.Generic;

namespace Express_Cafe_Core.Models;

public partial class DailyMenu
{
    public int DailyMenuId { get; set; }

    public DateOnly DailyMenuDate { get; set; }

    public decimal DemandQuantity { get; set; }

    public decimal CookedQuantity { get; set; }

    public decimal ServingQuantity { get; set; }

    public decimal Price { get; set; }

    public int RecipeId { get; set; }

    public virtual Recipe Recipe { get; set; } = null!;

    public virtual ICollection<SaleDetail> SaleDetails { get; set; } = new List<SaleDetail>();
}

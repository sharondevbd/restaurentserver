using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Express_Cafe_Core.Models;

public partial class DailyMenu
{
    public int DailyMenuId { get; set; }
    [DataType(DataType.Date)]
    public DateOnly? DailyMenuDate { get; set; }

    public int DemandQuantity { get; set; }

    public int? CookedQuantity { get; set; } = null;

    public int? ServingQuantity { get; set; } =null;

    public decimal? Price { get; set; } = null;

    public int RecipeId { get; set; }

    public virtual Recipe? Recipe { get; set; }

    public virtual ICollection<SaleDetail>? SaleDetails { get; set; } = new List<SaleDetail>();
}

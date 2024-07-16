using System;
using System.Collections.Generic;

namespace Express_Cafe_Core.Models;

public partial class Item
{
    public int ItemId { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<RecipeItem> RecipeItems { get; set; } = new List<RecipeItem>();

    public virtual ICollection<Requisition> Requisitions { get; set; } = new List<Requisition>();
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Express_Cafe_Core.Models;

public partial class Recipe
{
    public int RecipeId { get; set; }

    public string? RecipeName { get; set; }
    [NotMapped]
    public virtual ICollection<DailyMenu> DailyMenus { get; set; } = new List<DailyMenu>();

    public virtual ICollection<RecipeItem> RecipeItems { get; set; } = new List<RecipeItem>();
}

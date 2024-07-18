using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Express_Cafe_Core.Models;

public partial class RecipeItem
{
    public int RecipeItemId { get; set; }

    public decimal Quantity { get; set; }
	public string? Unit { get; set; }

	public int RecipeId { get; set; }
    //[ForeignKey("Item")]
    public int ItemId { get; set; }
	//[JsonInclude]
	//public virtual ICollection<Item>? Item { get; set; } = new List<Item>(); 
	public virtual Item? Item { get; set; }
    [JsonIgnore]
    [NotMapped]
    public virtual Recipe? Recipe { get; set; }
}

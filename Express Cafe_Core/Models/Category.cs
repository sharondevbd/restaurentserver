using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Express_Cafe_Core.Models
{
    [Table("MenuCategory")]
    public partial class Category:BaseDTO
    {
        public Category() { }

        //public int CategoryId { get; set; }
        public string CategoryName { get; set; } = null!;
        
      
    }
}

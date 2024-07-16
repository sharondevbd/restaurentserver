using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Express_Cafe_Core.Models
{
    public class BaseDTO
    {
        public int? Id { get; set; }
        [ForeignKey("Restaurent")]
        public int? ResId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string? UpdatedBy { get; set; }
        public bool IsActive { get; set; }
    }
}

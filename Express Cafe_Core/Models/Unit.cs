using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Express_Cafe_Core.Models
{
    public partial class Unit
    {
        public Unit() { }
       

        public int Id { get; set; }
        public string UnitName { get; set; } = null!;
        public string? ShortName { get; set; }
        //public int MeasurementUnit { get; set; }
        public int? ResId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string? UpdatedBy { get; set; }
    }
    public  class UnitConversion
    {
        public int Id { get; set; }
        [ForeignKey("Unit")]
        public int UNitId { get; set; }
        public int ConversionUNit { get; set; }
        public int ConversionQty { get; set; }
        public virtual Unit? Unit { get; set; }

    }
}

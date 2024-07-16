using System;
using System.Collections.Generic;

namespace Express_Cafe_Core.Models;

public partial class SaleHeader
{
    public int Id { get; set; }

    public string InvoiceNumber { get; set; } = null!;

    public string? CustomerName { get; set; }

    public string? CustomerEmail { get; set; }

    public string? CustomerPhone { get; set; }

    public DateOnly? SaleDate { get; set; }

    public decimal TotalPrice { get; set; }

    public decimal Vat { get; set; }

    public decimal TotalBill { get; set; }

    public virtual ICollection<SaleDetail> SaleDetails { get; set; } = new List<SaleDetail>();
}

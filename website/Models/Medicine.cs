using System;
using System.Collections.Generic;

namespace website.Models;

public partial class Medicine
{
    public decimal Medicineid { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public string? Manufacturer { get; set; }

    public decimal? Price { get; set; }

    public decimal? Stockquantity { get; set; }

    public DateTime? Expirydate { get; set; }

    public string? Imagepath { get; set; }

    public decimal? Discount { get; set; }

    public virtual ICollection<Cartitem> Cartitems { get; set; } = new List<Cartitem>();

    public virtual ICollection<Orderdetail> Orderdetails { get; set; } = new List<Orderdetail>();

    public virtual ICollection<Prescriptiondetail> Prescriptiondetails { get; set; } = new List<Prescriptiondetail>();

    public virtual ICollection<Purchasedetail> Purchasedetails { get; set; } = new List<Purchasedetail>();

    public virtual ICollection<Saledetail> Saledetails { get; set; } = new List<Saledetail>();
}

using System;
using System.Collections.Generic;

namespace website.Models;

public partial class Purchase
{
    public decimal Purchaseid { get; set; }

    public decimal? Supplierid { get; set; }

    public DateTime? Purchasedate { get; set; }

    public decimal? Totalamount { get; set; }

    public virtual ICollection<Purchasedetail> Purchasedetails { get; set; } = new List<Purchasedetail>();

    public virtual Supplier? Supplier { get; set; }
}

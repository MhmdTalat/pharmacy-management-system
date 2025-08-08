using System;
using System.Collections.Generic;

namespace website.Models;

public partial class Sale
{
    public decimal Saleid { get; set; }

    public decimal? Customerid { get; set; }

    public DateTime? Saledate { get; set; }

    public decimal? Totalamount { get; set; }

    public virtual Customer? Customer { get; set; }

    public virtual ICollection<Saledetail> Saledetails { get; set; } = new List<Saledetail>();
}

using System;
using System.Collections.Generic;

namespace website.Models;

public partial class Cart
{
    public decimal Cartid { get; set; }

    public decimal? Customerid { get; set; }

    public DateTime? Createdat { get; set; }

    public virtual ICollection<Cartitem> Cartitems { get; set; } = new List<Cartitem>();

    public virtual Customer? Customer { get; set; }
}

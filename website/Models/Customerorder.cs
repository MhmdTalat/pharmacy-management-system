using System;
using System.Collections.Generic;

namespace website.Models;

public partial class Customerorder
{
    public decimal Orderid { get; set; }

    public decimal? Customerid { get; set; }

    public DateTime? Orderdate { get; set; }

    public string? Status { get; set; }

    public decimal? Totalamount { get; set; }

    public virtual Customer? Customer { get; set; }

    public virtual ICollection<Orderdetail> Orderdetails { get; set; } = new List<Orderdetail>();
}

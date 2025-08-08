using System;
using System.Collections.Generic;

namespace website.Models;

public partial class Orderdetail
{
    public decimal Orderdetailid { get; set; }

    public decimal? Orderid { get; set; }

    public decimal? Medicineid { get; set; }

    public decimal? Quantity { get; set; }

    public decimal? Priceperunit { get; set; }

    public virtual Medicine? Medicine { get; set; }

    public virtual Customerorder? Order { get; set; }
}

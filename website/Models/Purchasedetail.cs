using System;
using System.Collections.Generic;

namespace website.Models;

public partial class Purchasedetail
{
    public decimal Purchasedetailid { get; set; }

    public decimal? Purchaseid { get; set; }

    public decimal? Medicineid { get; set; }

    public decimal? Quantity { get; set; }

    public decimal? Unitprice { get; set; }

    public virtual Medicine? Medicine { get; set; }

    public virtual Purchase? Purchase { get; set; }
}

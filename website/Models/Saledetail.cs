using System;
using System.Collections.Generic;

namespace website.Models;

public partial class Saledetail
{
    public decimal Saledetailid { get; set; }

    public decimal? Saleid { get; set; }

    public decimal? Medicineid { get; set; }

    public decimal? Quantity { get; set; }

    public decimal? Unitprice { get; set; }

    public virtual Medicine? Medicine { get; set; }

    public virtual Sale? Sale { get; set; }
}

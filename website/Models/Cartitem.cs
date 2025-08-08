using System;
using System.Collections.Generic;

namespace website.Models;

public partial class Cartitem
{
    public decimal Cartitemid { get; set; }

    public decimal? Cartid { get; set; }

    public decimal? Medicineid { get; set; }

    public decimal? Quantity { get; set; }

    public virtual Cart? Cart { get; set; }

    public virtual Medicine? Medicine { get; set; }
}

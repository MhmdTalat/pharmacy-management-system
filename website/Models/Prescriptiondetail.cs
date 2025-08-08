using System;
using System.Collections.Generic;

namespace website.Models;

public partial class Prescriptiondetail
{
    public decimal Prescriptiondetailid { get; set; }

    public decimal? Prescriptionid { get; set; }

    public decimal? Medicineid { get; set; }

    public string? Dosage { get; set; }

    public decimal? Quantity { get; set; }

    public virtual Medicine? Medicine { get; set; }

    public virtual Prescription? Prescription { get; set; }
}

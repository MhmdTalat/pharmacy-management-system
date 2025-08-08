using System;
using System.Collections.Generic;

namespace website.Models;

public partial class Prescription
{
    public decimal Prescriptionid { get; set; }

    public decimal? Customerid { get; set; }

    public string? Doctorname { get; set; }

    public DateTime? Validuntil { get; set; }

    public DateTime? Dateissued { get; set; }

    public virtual Customer? Customer { get; set; }

    public virtual ICollection<Prescriptiondetail> Prescriptiondetails { get; set; } = new List<Prescriptiondetail>();
}

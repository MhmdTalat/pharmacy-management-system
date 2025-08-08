using System;
using System.Collections.Generic;

namespace website.Models;

public partial class Supplier
{
    public decimal Supplierid { get; set; }

    public string? Suppliername { get; set; }

    public string? Contactperson { get; set; }

    public string? Phone { get; set; }

    public string? Email { get; set; }

    public string? Address { get; set; }

    public virtual ICollection<Purchase> Purchases { get; set; } = new List<Purchase>();
}

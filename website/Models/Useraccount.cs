using System;
using System.Collections.Generic;

namespace website.Models;

public partial class Useraccount
{
    public decimal Userid { get; set; }

    public string Username { get; set; } = null!;

    public string Passwordhash { get; set; } = null!;

    public string? Role { get; set; }

    public string? Fullname { get; set; }

    public string? Phone { get; set; }

    public string? Email { get; set; }
}

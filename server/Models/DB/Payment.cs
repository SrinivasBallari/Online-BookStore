using System;
using System.Collections.Generic;

namespace server.Models.DB;

public partial class Payment
{
    public int PaymentId { get; set; }

    public string? Status { get; set; }

    public string? PaymentType { get; set; }

    public decimal? Amount { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
using System;
using System.Collections.Generic;

namespace server.Models.DB;

public partial class Order
{
    public int OrderId { get; set; }

    public int? UserId { get; set; }

    public int? PaymentId { get; set; }

    public DateOnly? OrderDate { get; set; }

    public int? Total { get; set; }

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public virtual Payment? Payment { get; set; }

    public virtual User? User { get; set; }
}

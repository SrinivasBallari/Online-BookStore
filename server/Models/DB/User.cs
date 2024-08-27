using System;
using System.Collections.Generic;

namespace server.Models.DB;

public partial class User
{
    public int UserId { get; set; }

    public string? Name { get; set; }

    public string? Address { get; set; }

    public string? Contact { get; set; }

    public string? PinCode { get; set; }

    public string? Email { get; set; }

    public string? Password { get; set; }

    public string Role { get; set; } = "customer"; 

    public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
}
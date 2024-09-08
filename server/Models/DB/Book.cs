using System;
using System.Collections.Generic;

namespace server.Models.DB;

public partial class Book
{
    public int BookId { get; set; }

    public string? Title { get; set; }

    public int? AuthorId { get; set; }

    public int? PagesCount { get; set; }

    public string? Language { get; set; }

    public int? PublisherId { get; set; }

    public DateOnly? PublishedDate { get; set; }

    public double? PublishedVersion { get; set; }

    public decimal? Price { get; set; }

    public string? Description { get; set; }

    public string? ImageUrl { get; set; }

    public virtual Author? Author { get; set; }

    public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public virtual Publisher? Publisher { get; set; }

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

    public virtual ICollection<Tag> Tags { get; set; } = new List<Tag>();
}

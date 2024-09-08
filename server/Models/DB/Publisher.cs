using System;
using System.Collections.Generic;

namespace server.Models.DB;

public partial class Publisher
{
    public int PublisherId { get; set; }

    public string? PublisherName { get; set; }

    public string? PublisherAddress { get; set; }

    public virtual ICollection<Book> Books { get; set; } = new List<Book>();
}

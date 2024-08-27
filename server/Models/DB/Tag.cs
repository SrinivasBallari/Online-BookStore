using System;
using System.Collections.Generic;

namespace server.Models.DB;

public partial class Tag
{
    public int TagId { get; set; }

    public string? Tag1 { get; set; }

    public virtual ICollection<Book> Books { get; set; } = new List<Book>();
}
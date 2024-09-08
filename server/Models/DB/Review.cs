using System;
using System.Collections.Generic;

namespace server.Models.DB;

public partial class Review
{
    public int ReviewId { get; set; }

    public int? BookId { get; set; }

    public int? UserId { get; set; }

    public int? Rating { get; set; }

    public string? Review1 { get; set; }

    public DateOnly? ReviewedDate { get; set; }

    public virtual Book? Book { get; set; }

    public virtual User? User { get; set; }
}

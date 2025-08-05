using System;
using System.Collections.Generic;

namespace DBFirstProjectEFCore.Models;

public partial class WatchHistory
{
    public int HistoryId { get; set; }

    public int? UserId { get; set; }

    public int? MovieId { get; set; }

    public DateTime? WatchedOn { get; set; }

    public virtual Movie? Movie { get; set; }

    public virtual User? User { get; set; }
}

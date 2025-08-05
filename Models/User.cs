using System;
using System.Collections.Generic;

namespace DBFirstProjectEFCore.Models;

public partial class User
{
    public int UserId { get; set; }

    public string Username { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string Role { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

    public virtual ICollection<WatchHistory> WatchHistories { get; set; } = new List<WatchHistory>();
}

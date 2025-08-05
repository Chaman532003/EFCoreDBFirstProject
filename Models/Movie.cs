using System;
using System.Collections.Generic;

namespace DBFirstProjectEFCore.Models;

public partial class Movie
{
    public int MovieId { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public DateOnly? ReleaseDate { get; set; }

    public int? GenreId { get; set; }

    public int? DurationMinutes { get; set; }

    public string? PosterUrl { get; set; }

    public string? VideoUrl { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual Genre? Genre { get; set; }

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

    public virtual ICollection<WatchHistory> WatchHistories { get; set; } = new List<WatchHistory>();
}

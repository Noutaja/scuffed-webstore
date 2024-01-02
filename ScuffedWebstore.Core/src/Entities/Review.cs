using ScuffedWebstore.Core.src.Abstractions;

namespace ScuffedWebstore.Core.src.Entities;
public class Review : Timestamp
{
    public Guid UserID { get; set; }
    public Guid ProductID { get; set; }
    public string ReviewText { get; set; }
    public bool Likes { get; set; }
}
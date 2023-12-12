namespace ScuffedWebstore.Core.src.Abstractions;
public abstract class BaseEntity
{
    public Guid ID { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
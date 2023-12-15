namespace ScuffedWebstore.Core.src.Entities;
public abstract class BaseEntity
{
    public Guid ID { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
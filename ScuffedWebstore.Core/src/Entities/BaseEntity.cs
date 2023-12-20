namespace ScuffedWebstore.Core.src.Entities;
public abstract class BaseEntity : Timestamp
{
    public Guid ID { get; set; }
}
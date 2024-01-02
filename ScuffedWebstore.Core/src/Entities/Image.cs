using ScuffedWebstore.Core.src.Abstractions;

namespace ScuffedWebstore.Core.src.Entities;
public class Image : BaseEntity
{
    public Guid ProductID { get; set; }
    public string Url { get; set; }
}
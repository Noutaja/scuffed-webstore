using ScuffedWebstore.Core.src.Abstractions;

namespace ScuffedWebstore.Core.src.Entities;
public class Category : BaseEntity
{
    public string Name { get; set; }
    public string Url { get; set; }
}
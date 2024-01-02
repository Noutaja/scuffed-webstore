using ScuffedWebstore.Core.src.Entities;

namespace ScuffedWebstore.Core.src.Abstractions;
public interface IImageRepo : IBaseRepo<Image>
{
    public Task<IEnumerable<Image>> UpdateProductImages(IEnumerable<Image> updates, IEnumerable<Image> creates, IEnumerable<Image> deletes);
}
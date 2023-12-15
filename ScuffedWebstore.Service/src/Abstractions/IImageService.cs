using ScuffedWebstore.Core.src.Parameters;
using ScuffedWebstore.Service.src.DTOs;

namespace ScuffedWebstore.Service.src.Abstractions;
public interface IImageService
{
    public ImageReadDTO CreateOne(ImageCreateDTO address);
    public bool DeleteOne(Guid id);
    public IEnumerable<ImageReadDTO> GetAll(GetAllParams options);
    public ImageReadDTO? GetOneById(Guid id);
    public ImageReadDTO UpdateOne(Guid id, ImageUpdateDTO address);
}
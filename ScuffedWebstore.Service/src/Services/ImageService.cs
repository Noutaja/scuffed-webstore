using ScuffedWebstore.Core.src.Parameters;
using ScuffedWebstore.Service.src.Abstractions;
using ScuffedWebstore.Service.src.DTOs;

namespace ScuffedWebstore.Service.src.Services;
public class ImageService : IImageService
{
    public ImageReadDTO CreateOne(ImageCreateDTO address)
    {
        throw new NotImplementedException();
    }

    public bool DeleteOne(Guid id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<ImageReadDTO> GetAll(GetAllParams options)
    {
        throw new NotImplementedException();
    }

    public ImageReadDTO? GetOneById(Guid id)
    {
        throw new NotImplementedException();
    }

    public ImageReadDTO UpdateOne(Guid id, ImageUpdateDTO address)
    {
        throw new NotImplementedException();
    }
}
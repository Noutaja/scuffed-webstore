using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Moq;
using ScuffedWebstore.Core.src.Abstractions;
using ScuffedWebstore.Core.src.Entities;
using ScuffedWebstore.Core.src.Parameters;
using ScuffedWebstore.Service.src.DTOs;
using ScuffedWebstore.Service.src.Services;
using ScuffedWebstore.Service.src.Shared;
using Xunit;

namespace ScuffedWebstore.Test.src;
public class ImageServiceTest
{
    public ImageServiceTest()
    {

    }

    private static IMapper GetMapper()
    {
        MapperConfiguration mappingConfig = new MapperConfiguration(m =>
            {
                m.AddProfile(new MapperProfile());
            });
        IMapper mapper = mappingConfig.CreateMapper();
        return mapper;
    }

    [Theory]
    [ClassData(typeof(GetAllImagesData))]
    public async void GetAll_ShouldReturnValidResponse(IEnumerable<Image> response, IEnumerable<ImageReadDTO> expected)
    {
        Mock<IImageRepo> repo = new Mock<IImageRepo>();
        GetAllParams options = new GetAllParams();
        repo.Setup(repo => repo.GetAllAsync(options)).Returns(Task.FromResult(response));
        ImageService service = new ImageService(repo.Object, GetMapper());

        IEnumerable<ImageReadDTO> result = await service.GetAllAsync(options);

        Assert.Equivalent(expected, result);
    }

    public class GetAllImagesData : TheoryData<IEnumerable<Image>, IEnumerable<ImageReadDTO>>
    {
        public GetAllImagesData()
        {
            IEnumerable<Image> images = new List<Image>();
            Add(images, GetMapper().Map<IEnumerable<Image>, IEnumerable<ImageReadDTO>>(images));
        }
    }

    [Theory]
    [ClassData(typeof(GetOneByIDData))]
    public async void GetOneByID_ShouldReturnValidResponse(Image response, ImageReadDTO expected)
    {
        Mock<IImageRepo> repo = new Mock<IImageRepo>();
        repo.Setup(repo => repo.GetOneByIdAsync(It.IsAny<Guid>())).Returns(Task.FromResult(response));
        ImageService service = new ImageService(repo.Object, GetMapper());

        ImageReadDTO result = await service.GetOneByIDAsync(It.IsAny<Guid>());

        Assert.Equivalent(expected, response);

    }

    public class GetOneByIDData : TheoryData<Image, ImageReadDTO>
    {
        public GetOneByIDData()
        {
            Image image = new Image()
            {
                Url = "https://picsum.photos/200",
                ProductID = It.IsAny<Guid>()
            };
            Add(image, GetMapper().Map<Image, ImageReadDTO>(image));
            Add(null, null);
        }
    }

    [Theory]
    [ClassData(typeof(CreateOneImageData))]
    public async void CreateOne_ShouldReturnValidResponse(ImageCreateDTO input, Image response, ImageReadDTO expected)
    {
        Mock<IImageRepo> repo = new Mock<IImageRepo>();
        repo.Setup(repo => repo.CreateOneAsync(It.IsAny<Image>())).Returns(Task.FromResult(response));
        ImageService service = new ImageService(repo.Object, GetMapper());

        ImageReadDTO result = await service.CreateOneAsync(It.IsAny<Guid>(), input);

        Assert.Equivalent(expected, result);
    }

    public class CreateOneImageData : TheoryData<ImageCreateDTO, Image, ImageReadDTO>
    {
        public CreateOneImageData()
        {
            ImageCreateDTO imageInput = new ImageCreateDTO()
            {
                Url = "https://picsum.photos/200"
            };
            Image image = GetMapper().Map<ImageCreateDTO, Image>(imageInput);
            Add(imageInput, image, GetMapper().Map<Image, ImageReadDTO>(image));
        }
    }

    [Theory]
    [InlineData(true, true)]
    [InlineData(false, false)]
    public async void DeleteOne_ShouldReturnValidResponse(bool response, bool expected)
    {
        Mock<IImageRepo> repo = new Mock<IImageRepo>();
        repo.Setup(repo => repo.DeleteOneAsync(It.IsAny<Guid>())).Returns(Task.FromResult(response));
        ImageService service = new ImageService(repo.Object, GetMapper());

        bool result = await service.DeleteOneAsync(It.IsAny<Guid>());

        Assert.Equal(expected, response);
    }

    [Theory]
    [ClassData(typeof(UpdateOneImageData))]
    public async void UpdateOne_ShouldReturnValidResponse(ImageUpdateDTO? input, Image? foundImage, Image? response, ImageReadDTO? expected, Type? exception)
    {
        Mock<IImageRepo> repo = new Mock<IImageRepo>();
        repo.Setup(repo => repo.UpdateOneAsync(It.IsAny<Image>())).Returns(Task.FromResult(response));
        repo.Setup(repo => repo.GetOneByIdAsync(It.IsAny<Guid>())).Returns(Task.FromResult(foundImage));
        ImageService service = new ImageService(repo.Object, GetMapper());

        if (exception != null) Assert.ThrowsAsync(exception, async () => await service.UpdateOneAsync(It.IsAny<Guid>(), input));
        else
        {
            ImageReadDTO result = await service.UpdateOneAsync(It.IsAny<Guid>(), input);

            Assert.Equivalent(expected, result);
        }
    }

    public class UpdateOneImageData : TheoryData<ImageUpdateDTO?, Image?, Image?, ImageReadDTO?, Type?>
    {
        public UpdateOneImageData()
        {
            ImageUpdateDTO imageInput = new ImageUpdateDTO()
            {
                Url = "https://picsum.photos/200"
            };
            Image image = new Image()
            {
                Url = "https://picsum.photos/200",
                ProductID = It.IsAny<Guid>()
            };
            //Image image = GetMapper().Map<ImageUpdateDTO, Image>(imageInput);
            Add(imageInput, image, image, GetMapper().Map<Image, ImageReadDTO>(image), null);
            Add(imageInput, null, null, null, typeof(CustomException));
        }
    }
}

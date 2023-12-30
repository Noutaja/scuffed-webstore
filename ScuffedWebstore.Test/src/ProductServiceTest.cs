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
using Xunit.Abstractions;

namespace ScuffedWebstore.Test.src;

public class ProductServiceTest
{
    private ITestOutputHelper _output;
    public ProductServiceTest(ITestOutputHelper output)
    {
        _output = output;
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
    [ClassData(typeof(GetAllProductsData))]
    public async void GetAll_ShouldReturnValidResponse(IEnumerable<Product> response, IEnumerable<ProductReadDTO> expected)
    {
        Mock<IProductRepo> repo = new Mock<IProductRepo>();
        Mock<ICategoryRepo> categoryRepo = new Mock<ICategoryRepo>();
        Mock<IImageRepo> imageRepo = new Mock<IImageRepo>();
        GetAllParams options = new GetAllParams();
        repo.Setup(repo => repo.GetAllAsync(options)).Returns(Task.FromResult(response));
        ProductService service = new ProductService(repo.Object, categoryRepo.Object, imageRepo.Object, GetMapper());

        IEnumerable<ProductReadDTO> result = await service.GetAllAsync(options);

        Assert.Equivalent(expected, result);
    }

    public class GetAllProductsData : TheoryData<IEnumerable<Product>, IEnumerable<ProductReadDTO>>
    {
        public GetAllProductsData()
        {

            /* Product product = It.IsAny<Product>();
            {
                Title = "Product1",
                Description = "Description1",
                Price = 0,
                Inventory = 1,category
                CategoryID = It.IsAny<Guid>(),
                Category = It.IsAny<Category>(),
                Images = new List<Image>(),
                Reviews = new List<Review>(),
                OrderProducts = new List<OrderProduct>()
            }; */
            IEnumerable<Product> products = new List<Product>();
            Add(products, GetMapper().Map<IEnumerable<Product>, IEnumerable<ProductReadDTO>>(products));
        }
    }

    [Theory]
    [ClassData(typeof(GetOneByIDData))]
    public async void GetOneByID_ShouldReturnValidResponse(Product response, ProductReadDTO expected)
    {
        Mock<IProductRepo> repo = new Mock<IProductRepo>();
        Mock<ICategoryRepo> categoryRepo = new Mock<ICategoryRepo>();
        Mock<IImageRepo> imageRepo = new Mock<IImageRepo>();
        repo.Setup(repo => repo.GetOneByIdAsync(It.IsAny<Guid>())).Returns(Task.FromResult(response));
        ProductService service = new ProductService(repo.Object, categoryRepo.Object, imageRepo.Object, GetMapper());

        ProductReadDTO result = await service.GetOneByIDAsync(It.IsAny<Guid>());

        Assert.Equivalent(expected, result);
    }

    public class GetOneByIDData : TheoryData<Product, ProductReadDTO>
    {
        public GetOneByIDData()
        {
            Product product = new Product
            {
                Title = "Product1",
                Description = "Description1",
                Price = 0,
                Inventory = 1,
                CategoryID = It.IsAny<Guid>(),
                Category = It.IsAny<Category>(),
                Images = new List<Image>(),
                Reviews = new List<Review>(),
                OrderProducts = new List<OrderProduct>()
            };
            Add(product, GetMapper().Map<Product, ProductReadDTO>(product));
            Add(null, null);
        }
    }

    [Theory]
    [ClassData(typeof(CreateOneProductData))]
    public async void CreateOne_ShouldReturnValidResponse(Category foundCategory, ProductCreateDTO input, Product response, ProductReadDTO expected)
    {
        Mock<IProductRepo> repo = new Mock<IProductRepo>();
        Mock<ICategoryRepo> categoryRepo = new Mock<ICategoryRepo>();
        Mock<IImageRepo> imageRepo = new Mock<IImageRepo>();
        repo.Setup(repo => repo.CreateOneAsync(It.IsAny<Product>())).Returns(Task.FromResult(response));
        categoryRepo.Setup(repo => repo.GetOneByIdAsync(It.IsAny<Guid>())).Returns(Task.FromResult(foundCategory));
        ProductService service = new ProductService(repo.Object, categoryRepo.Object, imageRepo.Object, GetMapper());

        ProductReadDTO result = await service.CreateOneAsync(It.IsAny<Guid>(), input);

        Assert.Equivalent(expected, result);
    }

    public class CreateOneProductData : TheoryData<Category, ProductCreateDTO, Product, ProductReadDTO>
    {
        public CreateOneProductData()
        {
            ProductCreateDTO productInput = new ProductCreateDTO()
            {
                Title = "Product1",
                Description = "Description1",
                Price = 0,
                Inventory = 1,
                CategoryID = It.IsAny<Guid>(),
                Images = new List<ImageCreateDTO>(),
            };
            Product product = GetMapper().Map<ProductCreateDTO, Product>(productInput);
            Category category = new Category();
            Add(category, productInput, product, GetMapper().Map<Product, ProductReadDTO>(product));
        }
    }

    [Theory]
    [InlineData(true, true)]
    [InlineData(false, false)]
    public async void DeleteOne_ShouldReturnValidResponse(bool response, bool expected)
    {
        Mock<IProductRepo> repo = new Mock<IProductRepo>();
        Mock<ICategoryRepo> categoryRepo = new Mock<ICategoryRepo>();
        Mock<IImageRepo> imageRepo = new Mock<IImageRepo>();
        repo.Setup(repo => repo.DeleteOneAsync(It.IsAny<Guid>())).Returns(Task.FromResult(response));
        ProductService service = new ProductService(repo.Object, categoryRepo.Object, imageRepo.Object, GetMapper());

        bool result = await service.DeleteOneAsync(It.IsAny<Guid>());

        Assert.Equal(expected, response);
    }

    [Theory]
    [ClassData(typeof(UpdateOneProductData))]
    public async void UpdateOne_ShouldReturnValidResponse(ProductUpdateDTO? input, Product? foundProduct, Category? foundCategory, Product? response, ProductReadDTO? expected, Type? exception)
    {
        Mock<IProductRepo> repo = new Mock<IProductRepo>();
        Mock<ICategoryRepo> categoryRepo = new Mock<ICategoryRepo>();
        Mock<IImageRepo> imageRepo = new Mock<IImageRepo>();
        repo.Setup(repo => repo.UpdateOneAsync(It.IsAny<Product>())).Returns(Task.FromResult(response));
        repo.Setup(repo => repo.GetOneByIdAsync(It.IsAny<Guid>())).Returns(Task.FromResult(foundProduct));
        categoryRepo.Setup(repo => repo.GetOneByIdAsync(It.IsAny<Guid>())).Returns(Task.FromResult(foundCategory));
        imageRepo.Setup(repo => repo.UpdateProductImages(It.IsAny<IEnumerable<Image>>(), It.IsAny<IEnumerable<Image>>(), It.IsAny<IEnumerable<Image>>())).Returns(Task.FromResult(It.IsAny<IEnumerable<Image>>()));
        ProductService service = new ProductService(repo.Object, categoryRepo.Object, imageRepo.Object, GetMapper());

        if (exception != null) Assert.ThrowsAsync(exception, async () => await service.UpdateOneAsync(It.IsAny<Guid>(), input));
        else
        {
            ProductReadDTO result = await service.UpdateOneAsync(It.IsAny<Guid>(), input);

            Assert.Equivalent(expected, result);
        }
    }

    public class UpdateOneProductData : TheoryData<ProductUpdateDTO?, Product?, Category?, Product?, ProductReadDTO?, Type?>
    {
        public UpdateOneProductData()
        {
            ProductUpdateDTO productInput = new ProductUpdateDTO()
            {
                Title = "Product1",
                Description = "Description1",
                Price = 0,
                Inventory = 1,
                CategoryID = It.IsAny<Guid>(),
            };
            ProductUpdateDTO partialProductInput = new ProductUpdateDTO()
            {
                Title = "Product1"
            };

            Product product = new Product()
            {
                Title = "Product1",
                Description = "Description1",
                Price = 0,
                Inventory = 1,
                CategoryID = It.IsAny<Guid>(),
                Category = new Category(),
                Images = new List<Image>(),
                Reviews = new List<Review>(),
                OrderProducts = new List<OrderProduct>()
            };

            Category category = new Category();
            //Product product = GetMapper().Map<ProductUpdateDTO, Product>(productInput);
            Add(productInput, product, category, product, GetMapper().Map<Product, ProductReadDTO>(product), null);
            Add(partialProductInput, product, category, product, GetMapper().Map<Product, ProductReadDTO>(product), null);
            Add(productInput, null, category, null, GetMapper().Map<Product, ProductReadDTO>(product), typeof(CustomException));
        }
    }
}

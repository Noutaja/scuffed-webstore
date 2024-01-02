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
public class CategoryServiceTest
{
    public CategoryServiceTest()
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
    [ClassData(typeof(GetAllCategorysData))]
    public async void GetAll_ShouldReturnValidResponse(IEnumerable<Category> response, IEnumerable<CategoryReadDTO> expected)
    {
        Mock<ICategoryRepo> repo = new Mock<ICategoryRepo>();
        GetAllParams options = new GetAllParams();
        repo.Setup(repo => repo.GetAllAsync(options)).Returns(Task.FromResult(response));
        CategoryService service = new CategoryService(repo.Object, GetMapper());

        IEnumerable<CategoryReadDTO> result = await service.GetAllAsync(options);

        Assert.Equivalent(expected, result);
    }

    public class GetAllCategorysData : TheoryData<IEnumerable<Category>, IEnumerable<CategoryReadDTO>>
    {
        public GetAllCategorysData()
        {
            /* Category category1 = new Category() { FirstName = "Asd", LastName = "Asdeer", Email = "a@b.com", Password = "asdf1234", Avatar = "https://picsum.photos/200" };
            Category category2 = new Category() { FirstName = "Qwe", LastName = "Qwerty", Email = "q@b.com", Password = "asdf1234", Avatar = "https://picsum.photos/200" };
            Category category3 = new Category() { FirstName = "Zxc", LastName = "Zxcvbn", Email = "z@b.com", Password = "asdf1234", Avatar = "https://picsum.photos/200" }; */
            IEnumerable<Category> categorys = new List<Category>();
            Add(categorys, GetMapper().Map<IEnumerable<Category>, IEnumerable<CategoryReadDTO>>(categorys));
        }
    }

    [Theory]
    [ClassData(typeof(GetOneByIDData))]
    public async void GetOneByID_ShouldReturnValidResponse(Category response, CategoryReadDTO expected)
    {
        Mock<ICategoryRepo> repo = new Mock<ICategoryRepo>();
        repo.Setup(repo => repo.GetOneByIdAsync(It.IsAny<Guid>())).Returns(Task.FromResult(response));
        CategoryService service = new CategoryService(repo.Object, GetMapper());

        CategoryReadDTO result = await service.GetOneByIDAsync(It.IsAny<Guid>());

        Assert.Equivalent(expected, response);

    }

    public class GetOneByIDData : TheoryData<Category, CategoryReadDTO>
    {
        public GetOneByIDData()
        {
            Category category = new Category()
            {
                Name = "Name",
                Url = "https://picsum.photos/200"
            };
            Add(category, GetMapper().Map<Category, CategoryReadDTO>(category));
            Add(null, null);
        }
    }

    [Theory]
    [ClassData(typeof(CreateOneCategoryData))]
    public async void CreateOne_ShouldReturnValidResponse(CategoryCreateDTO input, Category? response, CategoryReadDTO? expected, Type? exception)
    {
        Mock<ICategoryRepo> repo = new Mock<ICategoryRepo>();
        repo.Setup(repo => repo.CreateOneAsync(It.IsAny<Category>())).Returns(Task.FromResult(response));
        CategoryService service = new CategoryService(repo.Object, GetMapper());

        if (exception != null) await Assert.ThrowsAsync(exception, async () => await service.CreateOneAsync(It.IsAny<Guid>(), input));
        else
        {
            CategoryReadDTO result = await service.CreateOneAsync(It.IsAny<Guid>(), input);

            Assert.Equivalent(expected, result);
        }
    }

    public class CreateOneCategoryData : TheoryData<CategoryCreateDTO, Category?, CategoryReadDTO?, Type?>
    {
        public CreateOneCategoryData()
        {
            CategoryCreateDTO categoryInput = new CategoryCreateDTO()
            {
                Name = "Name",
                Url = "https://picsum.photos/200"
            };
            CategoryCreateDTO invalidCategoryInput = new CategoryCreateDTO()
            {
                Name = "Name",
                Url = "httpspicsum.photos/200"
            };
            Category category = GetMapper().Map<CategoryCreateDTO, Category>(categoryInput);
            Add(categoryInput, category, GetMapper().Map<Category, CategoryReadDTO>(category), null);
            Add(invalidCategoryInput, null, null, typeof(CustomException));
        }
    }

    [Theory]
    [InlineData(true, true)]
    [InlineData(false, false)]
    public async void DeleteOne_ShouldReturnValidResponse(bool response, bool expected)
    {
        Mock<ICategoryRepo> repo = new Mock<ICategoryRepo>();
        repo.Setup(repo => repo.DeleteOneAsync(It.IsAny<Guid>())).Returns(Task.FromResult(response));
        CategoryService service = new CategoryService(repo.Object, GetMapper());

        bool result = await service.DeleteOneAsync(It.IsAny<Guid>());

        Assert.Equal(expected, response);
    }

    [Theory]
    [ClassData(typeof(UpdateOneCategoryData))]
    public async void UpdateOne_ShouldReturnValidResponse(CategoryUpdateDTO? input, Category? foundCategory, Category? response, CategoryReadDTO? expected, Type? exception)
    {
        Mock<ICategoryRepo> repo = new Mock<ICategoryRepo>();
        repo.Setup(repo => repo.UpdateOneAsync(It.IsAny<Category>())).Returns(Task.FromResult(response));
        repo.Setup(repo => repo.GetOneByIdAsync(It.IsAny<Guid>())).Returns(Task.FromResult(foundCategory));
        CategoryService service = new CategoryService(repo.Object, GetMapper());

        if (exception != null) Assert.ThrowsAsync(exception, async () => await service.UpdateOneAsync(It.IsAny<Guid>(), input));
        else
        {
            CategoryReadDTO result = await service.UpdateOneAsync(It.IsAny<Guid>(), input);

            Assert.Equivalent(expected, result);
        }
    }

    public class UpdateOneCategoryData : TheoryData<CategoryUpdateDTO?, Category?, Category?, CategoryReadDTO?, Type?>
    {
        public UpdateOneCategoryData()
        {
            CategoryUpdateDTO categoryInput = new CategoryUpdateDTO()
            {
                Name = "Name",
                Url = "https://picsum.photos/200"
            };
            Category category = new Category()
            {
                Name = "Name",
                Url = "https://picsum.photos/200"
            };
            CategoryUpdateDTO partialCategoryInput = new CategoryUpdateDTO()
            {
                Name = "Name"
            };
            CategoryUpdateDTO invalidCategoryInput = new CategoryUpdateDTO()
            {
                Name = "Name",
                Url = "httpspicsum.photos/200"
            };
            Add(categoryInput, category, category, GetMapper().Map<Category, CategoryReadDTO>(category), null);
            Add(partialCategoryInput, category, category, GetMapper().Map<Category, CategoryReadDTO>(category), null);
            Add(categoryInput, null, null, null, typeof(CustomException));
            Add(invalidCategoryInput, category, null, null, typeof(CustomException));
        }
    }
}

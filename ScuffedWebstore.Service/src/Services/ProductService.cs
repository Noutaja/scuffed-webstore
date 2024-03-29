using System.Reflection;
using AutoMapper;
using ScuffedWebstore.Core.src.Abstractions;
using ScuffedWebstore.Core.src.Entities;
using ScuffedWebstore.Core.src.Parameters;
using ScuffedWebstore.Service.src.Abstractions;
using ScuffedWebstore.Service.src.DTOs;
using ScuffedWebstore.Service.src.Shared;

namespace ScuffedWebstore.Service.src.Services;
public class ProductService : BaseService<Product, ProductReadDTO, ProductCreateDTO, ProductUpdateDTO>, IProductService
{
    private ICategoryRepo _categoryRepo;
    private IImageRepo _imageRepo;
    public ProductService(IProductRepo repo, ICategoryRepo categoryRepo, IImageRepo imageRepo, IMapper mapper) : base(repo, mapper)
    {
        _categoryRepo = categoryRepo;
        _imageRepo = imageRepo;
    }

    public override async Task<ProductReadDTO> CreateOneAsync(Guid id, ProductCreateDTO createObject)
    {
        if (createObject.Title.Length < 3)
            throw CustomException.InvalidParameters("Title can't be less than 3 characters long");
        if (createObject.Description.Length < 10)
            throw CustomException.InvalidParameters("Description can't be less than 10 characters long");
        if (createObject.Price < 0)
            throw CustomException.InvalidParameters("Price can't be less than 0");
        if (createObject.Inventory < 0)
            throw CustomException.InvalidParameters("Inventory can't be less than 0");
        if (createObject.Images.Count() < 1)
            throw CustomException.InvalidParameters("Product must have an image");

        Category? c = await _categoryRepo.GetOneByIdAsync(createObject.CategoryID);
        if (c == null) throw CustomException.NotFoundException("Category not found");

        Product product = _mapper.Map<ProductCreateDTO, Product>(createObject);
        product.ID = id;

        return _mapper.Map<Product, ProductReadDTO>(await _repo.CreateOneAsync(product));
    }

    public override async Task<ProductReadDTO> UpdateOneAsync(Guid id, ProductUpdateDTO updateObject)
    {
        if (updateObject.Title != null && updateObject.Title.Length < 3)
            throw CustomException.InvalidParameters("Title can't be less than 3 characters long");
        if (updateObject.Description != null && updateObject.Description.Length < 10)
            throw CustomException.InvalidParameters("Description can't be less than 10 characters long");
        if (updateObject.Price != null && updateObject.Price < 0)
            throw CustomException.InvalidParameters("Price can't be less than 0");
        if (updateObject.Inventory != null && updateObject.Inventory < 0)
            throw CustomException.InvalidParameters("Inventory can't be less than 0");

        Product? currentEntity = await _repo.GetOneByIdAsync(id);
        if (currentEntity == null) throw CustomException.NotFoundException("Entity to update not Found");

        if (updateObject.Title != null) currentEntity.Title = updateObject.Title;
        if (updateObject.Description != null) currentEntity.Description = updateObject.Description;
        if (updateObject.Price != null) currentEntity.Price = (double)updateObject.Price;
        if (updateObject.Inventory != null) currentEntity.Inventory = (int)updateObject.Inventory;
        if (updateObject.CategoryID != null) currentEntity.Category.ID = (Guid)updateObject.CategoryID;

        Category? c = await _categoryRepo.GetOneByIdAsync(currentEntity.Category.ID);
        if (c == null) throw CustomException.NotFoundException("Category not found");
        currentEntity.Category = c;

        List<Image> updates = new List<Image>();
        if (updateObject.UpdatedImages != null && updateObject.UpdatedImages.Count() > 0)
        {
            foreach (ImageUpdateDTO i in updateObject.UpdatedImages)
            {
                Image? img = await _imageRepo.GetOneByIdAsync((Guid)i.ID);
                if (img == null) throw CustomException.NotFoundException("Image not found");

                if (i.Url != null && i.Url.Length > 0) img.Url = i.Url;

                updates.Add(img);
            }
        }

        List<Image> creates = new List<Image>();
        if (updateObject.NewImages != null && updateObject.NewImages.Count() > 0)
        {
            foreach (ImageCreateDTO i in updateObject.NewImages)
            {
                Image img = _mapper.Map<ImageCreateDTO, Image>(i);
                img.ProductID = currentEntity.ID;
                creates.Add(img);
            }
        }

        List<Image> deletes = new List<Image>();
        if (updateObject.DeletedImages != null && updateObject.DeletedImages.Count() > 0)
        {
            foreach (Guid i in updateObject.DeletedImages)
            {
                Image? img = await _imageRepo.GetOneByIdAsync(i);
                if (img == null) throw CustomException.NotFoundException("Image not found");

                deletes.Add(img);
            }
        }

        currentEntity.Images = await _imageRepo.UpdateProductImages(updates, creates, deletes);

        return _mapper.Map<Product, ProductReadDTO>(await _repo.UpdateOneAsync(currentEntity));
    }
}
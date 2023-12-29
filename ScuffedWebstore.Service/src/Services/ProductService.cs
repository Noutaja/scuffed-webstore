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
        Category? c = await _categoryRepo.GetOneByIdAsync(createObject.CategoryID);
        if (c == null) throw CustomException.NotFoundException("Category not found");

        Product product = _mapper.Map<ProductCreateDTO, Product>(createObject);
        product.ID = id;

        return _mapper.Map<Product, ProductReadDTO>(await _repo.CreateOneAsync(product));
    }

    public override async Task<ProductReadDTO> UpdateOneAsync(Guid id, ProductUpdateDTO updateObject)
    {
        Product? currentEntity = await _repo.GetOneByIdAsync(id);
        if (currentEntity == null) throw CustomException.NotFoundException("Entity to update not Found");

        if (updateObject.Title != null) currentEntity.Title = updateObject.Title;
        if (updateObject.Description != null) currentEntity.Description = updateObject.Description;
        if (updateObject.Price != null) currentEntity.Price = (double)updateObject.Price;
        if (updateObject.Inventory != null) currentEntity.Inventory = (int)updateObject.Inventory;
        if (updateObject.CategoryID != null) currentEntity.CategoryID = (Guid)updateObject.CategoryID;

        if (updateObject.UpdatedImages != null && updateObject.UpdatedImages.Count() > 0)
        {
            foreach (ImageUpdateDTO i in updateObject.UpdatedImages)
            {
                Image? img = await _imageRepo.GetOneByIdAsync((Guid)i.ID);
                if (img == null) throw CustomException.NotFoundException("Image not found");

                if (i.Url != null && i.Url.Length > 0) img.Url = i.Url;

                await _imageRepo.UpdateOneAsync(img);
            }
        }

        if (updateObject.NewImages != null && updateObject.NewImages.Count() > 0)
        {
            foreach (ImageCreateDTO i in updateObject.NewImages)
            {
                await _imageRepo.CreateOneAsync(_mapper.Map<ImageCreateDTO, Image>(i));
            }
        }

        if (updateObject.DeletedImages != null && updateObject.DeletedImages.Count() > 0)
        {
            foreach (Guid i in updateObject.DeletedImages)
            {
                await _imageRepo.DeleteOneAsync(i);
            }
        }

        /* foreach (PropertyInfo prop in updateObject.GetType().GetProperties())
        {
            if (prop.GetValue(updateObject) == null) continue;

            if (prop.GetType() == typeof(IEnumerable<ImageUpdateDTO>))
            {

            }

            PropertyInfo? editedProp = currentEntity.GetType().GetProperty(prop.Name);
            editedProp.SetValue(currentEntity, prop.GetValue(updateObject));
        } */


        //T updatedEntity = _mapper.Map<TUpdateDTO, T>(updateObject, currentEntity);

        return _mapper.Map<Product, ProductReadDTO>(await _repo.UpdateOneAsync(currentEntity));
    }
}
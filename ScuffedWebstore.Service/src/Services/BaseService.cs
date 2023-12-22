using AutoMapper;
using ScuffedWebstore.Core.src.Abstractions;
using ScuffedWebstore.Core.src.Entities;
using ScuffedWebstore.Core.src.Parameters;
using ScuffedWebstore.Service.src.Abstractions;
using ScuffedWebstore.Service.src.Shared;

namespace ScuffedWebstore.Service.src.Services;
public class BaseService<T, TReadDTO, TCreateDTO, TUpdateDTO> : IBaseService<T, TReadDTO, TCreateDTO, TUpdateDTO> where T : BaseEntity
{
    protected IBaseRepo<T> _repo;
    protected IMapper _mapper;

    public BaseService(IBaseRepo<T> repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    public virtual async Task<TReadDTO> CreateOneAsync(Guid id, TCreateDTO createObject)
    {
        T t = _mapper.Map<TCreateDTO, T>(createObject);
        t.ID = id;
        return _mapper.Map<T, TReadDTO>(await _repo.CreateOneAsync(t));
    }

    public virtual async Task<bool> DeleteOneAsync(Guid id)
    {
        return await _repo.DeleteOneAsync(id);
    }

    public virtual async Task<IEnumerable<TReadDTO>> GetAllAsync(GetAllParams getAllParams)
    {
        return _mapper.Map<IEnumerable<T>, IEnumerable<TReadDTO>>(await _repo.GetAllAsync(getAllParams));
    }

    public virtual async Task<TReadDTO?> GetOneByIDAsync(Guid id)
    {
        T? t = await _repo.GetOneByIdAsync(id);
        if (t == null) return default;

        return _mapper.Map<T, TReadDTO>(t);
    }

    public virtual async Task<TReadDTO> UpdateOneAsync(Guid id, TUpdateDTO updateObject)
    {
        T? currentEntity = await _repo.GetOneByIdAsync(id);
        if (currentEntity == null) throw CustomException.NotFoundException("Not Found");

        T updatedEntity = _mapper.Map<TUpdateDTO, T>(updateObject, currentEntity);

        return _mapper.Map<T, TReadDTO>(await _repo.UpdateOneAsync(updatedEntity));
    }
}
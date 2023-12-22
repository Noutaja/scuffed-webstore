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

    public virtual TReadDTO CreateOne(Guid id, TCreateDTO createObject)
    {
        T t = _mapper.Map<TCreateDTO, T>(createObject);
        t.ID = id;
        return _mapper.Map<T, TReadDTO>(_repo.CreateOne(t));
    }

    public virtual bool DeleteOne(Guid id)
    {
        return _repo.DeleteOne(id);
    }

    public virtual IEnumerable<TReadDTO> GetAll(GetAllParams getAllParams)
    {
        return _mapper.Map<IEnumerable<T>, IEnumerable<TReadDTO>>(_repo.GetAll(getAllParams));
    }

    public virtual TReadDTO? GetOneByID(Guid id)
    {
        T? t = _repo.GetOneById(id);
        if (t == null) return default;

        return _mapper.Map<T, TReadDTO>(t);
    }

    public virtual TReadDTO UpdateOne(Guid id, TUpdateDTO updateObject)
    {
        T currentEntity = _repo.GetOneById(id);
        if (currentEntity == null) throw CustomException.NotFoundException("Not Found");

        T updatedEntity = _mapper.Map<TUpdateDTO, T>(updateObject, currentEntity);

        return _mapper.Map<T, TReadDTO>(_repo.UpdateOne(updatedEntity));
    }
}
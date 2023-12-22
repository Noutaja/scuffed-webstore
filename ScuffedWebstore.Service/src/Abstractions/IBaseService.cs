using ScuffedWebstore.Core.src.Entities;
using ScuffedWebstore.Core.src.Parameters;

namespace ScuffedWebstore.Service.src.Abstractions;
public interface IBaseService<T, TReadDTO, TCreateDTO, TUpdateDTO> where T : BaseEntity
{
    public IEnumerable<TReadDTO> GetAll(GetAllParams getAllParams);
    public TReadDTO? GetOneByID(Guid id);
    public TReadDTO UpdateOne(Guid id, TUpdateDTO updateObject);
    public bool DeleteOne(Guid id);
    public TReadDTO CreateOne(Guid id, TCreateDTO createObject);
}
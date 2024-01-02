using ScuffedWebstore.Core.src.Entities;
using ScuffedWebstore.Core.src.Parameters;

namespace ScuffedWebstore.Service.src.Abstractions;
public interface IBaseService<T, TReadDTO, TCreateDTO, TUpdateDTO> where T : BaseEntity
{
    public Task<IEnumerable<TReadDTO>> GetAllAsync(GetAllParams getAllParams);
    public Task<TReadDTO?> GetOneByIDAsync(Guid id);
    public Task<TReadDTO> UpdateOneAsync(Guid id, TUpdateDTO updateObject);
    public Task<bool> DeleteOneAsync(Guid id);
    public Task<TReadDTO> CreateOneAsync(Guid id, TCreateDTO createObject);
}
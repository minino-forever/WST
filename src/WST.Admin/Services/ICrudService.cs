using System;
using System.Threading.Tasks;
using WST.Admin.Models;

namespace WST.Admin.Services
{
    public interface ICrudService<TEntity, TFormDto, TListDto>
        where TEntity : BaseEntity
    {
        Task<TFormDto> Get(Guid id);
        
        Task<TFormDto[]> GetAll();
        
        Task<TEntity> Create(TFormDto formDto);
        
        Task Delete(Guid id);
    }
}
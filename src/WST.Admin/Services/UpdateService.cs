using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WST.Admin.Models;

namespace WST.Admin.Services
{
    /// <summary>Сервис обновления данных</summary>
    /// <typeparam name="T">Тип сущности</typeparam>
    public interface IUpdateService<T>
        where T : BaseEntity
    {
        /// <summary>Обновить сущность в БД</summary>
        /// <param name="entity">Сущность для обновления</param>
        /// <returns>Обновленная сущность</returns>
        Task<T> Update(T entity);

        /// <summary>Обновить коллекцию сущностей в БД</summary>
        /// <param name="entities">Сущности для обновления</param>
        /// <returns>Обновленные сущности</returns>
        Task<IEnumerable<T>> UpdateMany(IReadOnlyList<T> entities);
    }
    
    /// <inheritdoc cref="IUpdateService<T>"/>
    public class UpdateService<T> : IUpdateService<T>
        where T : BaseEntity
    {
        private readonly WstDbContext _context;

        public UpdateService(WstDbContext context)
        {
            _context = context;
        }
        
        public async Task<T> Update(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentException("Передано значение null");
            }

            _context.Set<T>().Update(entity);

            await _context.SaveChangesAsync();
            
            return entity;
        }

        public async Task<IEnumerable<T>> UpdateMany(IReadOnlyList<T> entities)
        {
            if (entities == null)
            {
                throw new ArgumentException("Передано значение null");
            }

            if (!entities.Any())
            {
                return new T[0];
            }

            _context.Set<T>().UpdateRange(entities);

            await _context.SaveChangesAsync();

            return entities;
        }
    }
}
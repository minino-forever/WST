using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using WST.Admin.Models;

namespace WST.Admin.Services
{
    /// <summary>Сервис создания данных</summary>
    /// <typeparam name="T">Тип сущности</typeparam>
    public interface ICreateService<T>
        where T : BaseEntity
    {
        /// <summary>Создать сущность в БД</summary>
        /// <param name="entity">Сущность для сохранения</param>
        /// <returns>Сохраненная сущность</returns>
        Task<T> Create(T entity);

        /// <summary>Создать коллекцию сущностей в БД</summary>
        /// <param name="entities">Сущности для сохранения</param>
        /// <returns>Сохраненные сущности</returns>
        Task<IEnumerable<T>> CreateMany(IReadOnlyList<T> entities);
    }

    /// <inheritdoc cref="ICreateService<T>"/>
    public class CreateService<T> : ICreateService<T>
        where T : BaseEntity
    {
        private readonly IServiceProvider _serviceProvider;

        public CreateService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        
        public async Task<T> Create(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentException("Передано значение null");
            }

            if (entity.Id == default)
            {
                entity.Id = Guid.NewGuid();
            }

            using (var ctx = _serviceProvider.GetService<WstDbContext>())
            {
                await ctx.Set<T>().AddAsync(entity);

                await ctx.SaveChangesAsync();    
            }
            
            return entity;
        }

        public async Task<IEnumerable<T>> CreateMany(IReadOnlyList<T> entities)
        {
            if (entities == null)
            {
                throw new ArgumentException("Передано значение null");
            }

            if (!entities.Any())
            {
                return new T[0];
            }

            using (var ctx = _serviceProvider.GetService<WstDbContext>())
            {
                foreach (var entity in entities)
                {
                    if (entity.Id == default)
                    {
                        entity.Id = Guid.NewGuid();
                    }
            
                    ctx.Set<T>().Add(entity);
                }
                
                await ctx.SaveChangesAsync();
            }
            
            return entities;
        }
    }
}
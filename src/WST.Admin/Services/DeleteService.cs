using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using WST.Admin.Models;

namespace WST.Admin.Services
{
    /// <summary>Сервис удаления данных</summary>
    /// <typeparam name="T">Тип сущности</typeparam>
    public interface IDeleteService<T>
        where T : BaseEntity
    {
        /// <summary>Удалить сущность из БД</summary>
        /// <param name="id">Id удаляемой сущности</param>
        Task Delete(Guid id);

        /// <summary>Удалить коллекцию сущностей в БД</summary>
        /// <param name="entities">Сущности для удаления</param>
        Task DeleteMany(IReadOnlyList<T> entities);
    }
    
    /// <inheritdoc cref="IUpdateService<T>"/>
    public class DeleteService<T> : IDeleteService<T>
        where T : BaseEntity, new()
    {
        private readonly IServiceProvider _serviceProvider;

        public DeleteService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        
        public async Task Delete(Guid id)
        {
            using (var ctx = _serviceProvider.GetService<WstDbContext>())
            {
                ctx.Set<T>().Remove(new T { Id = id});
                
                await ctx.SaveChangesAsync();
            }
        }

        public async Task DeleteMany(IReadOnlyList<T> entities)
        {
            if (entities == null)
            {
                throw new ArgumentException("Передано значение null");
            }

            if (!entities.Any())
            {
                return;
            }

            using (var ctx = _serviceProvider.GetService<WstDbContext>())
            {
                ctx.Set<T>().RemoveRange(entities);

                await ctx.SaveChangesAsync();
            }
        }
    }
}
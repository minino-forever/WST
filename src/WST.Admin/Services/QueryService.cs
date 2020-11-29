using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WST.Admin.Models;

namespace WST.Admin.Services
{
    /// <summary>Сервис получения данных</summary>
    /// <typeparam name="T">Тип сущности</typeparam>
    public interface IQueryService<T>
        where T : BaseEntity
    {
        /// <summary>Получить сущность по идентификатору</summary>
        /// <param name="id">Идентификатор сущности</param>
        /// <returns>Сущность</returns>
        Task<T> Get(Guid id);

        /// <summary>Получить все сущности</summary>
        /// <returns>Все сущности</returns>
        IQueryable<T> GetAll();
    }

    public class QueryService<T> : IQueryService<T>
        where T : BaseEntity
    {
        private readonly IServiceProvider _serviceProvider;

        public QueryService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task<T> Get(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException("Передан невалидный идентификатор");
            }

            T entity;

            using (var ctx = _serviceProvider.GetService<WstDbContext>())
            {
                entity = await ctx.FindAsync<T>(id);
            }

            return entity;
        }

        public IQueryable<T> GetAll()
        {
            var ctx = _serviceProvider.GetService<WstDbContext>();

            return ctx.Set<T>();
        }
    }
}
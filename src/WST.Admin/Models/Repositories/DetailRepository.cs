using System;
using System.Linq;

namespace WST.Admin.Models.Repositories
{
    public interface IDetailRepository
    {
        IQueryable<Detail> Details { get; }

        void Save(Detail detail);

        void Delete(Guid id);
    }
    
    public class DetailRepository : IDetailRepository
    {
        private readonly WstDbContext _ctx;

        public DetailRepository(WstDbContext ctx)
        {
            _ctx = ctx;
        }

        public IQueryable<Detail> Details => _ctx.Detail;
        
        public void Save(Detail detail)
        {
            if (detail.Id == Guid.Empty)
            {
                _ctx.Detail.Add(detail);
            }
            else
            {
                var dbEntry = _ctx.Detail.FirstOrDefault(d => d.Id == detail.Id);

                if (dbEntry != null)
                {
                    dbEntry.Amount = detail.Amount;
                    dbEntry.Description = detail.Description;
                    dbEntry.Name = detail.Name;
                    dbEntry.Number = detail.Number;
                }
            }

            _ctx.SaveChanges();
        }

        public void Delete(Guid id)
        {
            var dbEntry = _ctx.Detail.FirstOrDefault(d => d.Id == id);

            if (dbEntry != null)
            {
                _ctx.Detail.Remove(dbEntry);

                _ctx.SaveChanges();
            }
        }
    }
}
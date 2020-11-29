using System;
using System.Linq;

namespace WST.Admin.Models.Repositories
{
    public interface IBreakingRepository
    {
        IQueryable<Breaking> Breakings { get; }

        void Save(Breaking breaking);
        
        void Delete(Guid breakingId);
    }
    
    public class BreakingRepository : IBreakingRepository
    {
        private readonly WstDbContext _ctx;

        public BreakingRepository(WstDbContext ctx)
        {
            _ctx = ctx;
        }

        public IQueryable<Breaking> Breakings => _ctx.Breaking;
        
        public void Save(Breaking breaking)
        {
            if (breaking.Id == Guid.Empty)
            {
                breaking.CreateDate = DateTime.UtcNow;
                
                _ctx.Breaking.Add(breaking);
            }
            else
            {
                var dbEntry = _ctx.Breaking.FirstOrDefault(p => p.Id == breaking.Id);

                if (dbEntry != null)
                {
                    dbEntry.Description = breaking.Description;
                    dbEntry.RepairMethod = breaking.RepairMethod;
                }
            }

            _ctx.SaveChanges();
        }

        public void Delete(Guid breakingId)
        {
            var dbEntry = _ctx.Breaking.FirstOrDefault(p => p.Id == breakingId);

            if (dbEntry != null)
            {
                _ctx.Breaking.Remove(dbEntry);

                _ctx.SaveChanges();
            }
        }
    }
}
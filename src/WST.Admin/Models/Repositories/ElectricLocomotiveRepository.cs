using System;
using System.Linq;

namespace WST.Admin.Models.Repositories
{
    public interface IElectricLocomotiveRepository
    {
        IQueryable<ElectricLocomotive> ElectricLocomotives { get; }

        void Save(ElectricLocomotive electricLocomotive);

        void Delete(Guid id);
    }
    
    public class ElectricLocomotiveRepository : IElectricLocomotiveRepository
    {
        private readonly WstDbContext context;

        public ElectricLocomotiveRepository(WstDbContext ctx)
        {
            context = ctx;
        }

        public IQueryable<ElectricLocomotive> ElectricLocomotives => context.ElectricLocomotive;
        
        public void Save(ElectricLocomotive electricLocomotive)
        {
            if (electricLocomotive.Id == Guid.Empty)
            {
                electricLocomotive.CreateDate = DateTime.UtcNow;
                
                context.ElectricLocomotive.Add(electricLocomotive);
            }
            else
            {
                var dbEntry = context.ElectricLocomotive.FirstOrDefault(p => p.Id == electricLocomotive.Id);

                if (dbEntry != null)
                {
                    dbEntry.Modification = electricLocomotive.Modification;
                    dbEntry.Power = electricLocomotive.Power;
                    dbEntry.PinCount = electricLocomotive.PinCount;
                    dbEntry.SectionCount = electricLocomotive.SectionCount;
                    dbEntry.SerialNumber = electricLocomotive.SerialNumber;
                    dbEntry.UniqueNumber = electricLocomotive.UniqueNumber;
                }
            }

            context.SaveChanges();
        }

        public void Delete(Guid id)
        {
            var dbEntry = context.ElectricLocomotive.FirstOrDefault(p => p.Id == id);

            if (dbEntry != null)
            {
                context.ElectricLocomotive.Remove(dbEntry);

                context.SaveChanges();
            }
        }
    }
}
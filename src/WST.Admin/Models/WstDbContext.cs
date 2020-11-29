using Microsoft.EntityFrameworkCore;

namespace WST.Admin.Models
{
    /// <summary>Контекст данных</summary>
    public class WstDbContext : DbContext
    {
        public WstDbContext(DbContextOptions<WstDbContext> options)
            : base(options)
        {
            
        }
        
        /// <summary>Пользователь</summary>
        public DbSet<User> User { get; set; }

        /// <summary>Электровоз</summary>
        public DbSet<ElectricLocomotive> ElectricLocomotive { get; set; }

        /// <summary>Неисправность</summary>
        public DbSet<Breaking> Breaking { get; set; }
        
        /// <summary>Изображение поломки</summary>
        public DbSet<BreakingImage> BreakingImage { get; set; }
        
        /// <summary>Деталь</summary>
        public DbSet<Detail> Detail { get; set; }

        /// <summary>Связь электровоза и поломок</summary>
        public DbSet<ElectricLocomotiveBreakingProxy> ElectricLocomotiveBreakingProxy { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ElectricLocomotiveBreakingProxy>().HasKey(vf=> new {vf.BreakingId, vf.ElectricLocomotiveId});
        }
    }
}
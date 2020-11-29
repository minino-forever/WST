using System.Collections.Generic;
using System.Linq;

namespace WST.Admin.Models.Repositories
{
    public interface IBreakingImageRepository
    {
        IQueryable<BreakingImage> BreakingImages { get; }

        void Save(IReadOnlyList<BreakingImage> breakingImages);
    }

    public class BreakingImageRepository : IBreakingImageRepository
    {
        private readonly WstDbContext _ctx;

        public BreakingImageRepository(WstDbContext ctx)
        {
            _ctx = ctx;
        }

        public IQueryable<BreakingImage> BreakingImages => _ctx.BreakingImage;
        
        public void Save(IReadOnlyList<BreakingImage> breakingImages)
        {
            _ctx.BreakingImage.AddRange(breakingImages);

            _ctx.SaveChanges();
        }
    }
}
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace WST.Admin.Models
{
    public static class MigrationHelper
    {
        public static void Migrate(IApplicationBuilder app)
        {
            var context = app.ApplicationServices.GetRequiredService<WstDbContext>();

            context.Database.Migrate();
        }
    }
}
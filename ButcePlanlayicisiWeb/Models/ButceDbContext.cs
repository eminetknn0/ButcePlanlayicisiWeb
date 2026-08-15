using Microsoft.EntityFrameworkCore;
using ButcePlanlayicisiWeb.Models;

namespace ButcePlanlayicisiWeb.Models
{
    public class ButceDbContext : DbContext
    {
        public ButceDbContext(DbContextOptions<ButceDbContext> options)
            : base(options)
        {
        }

        public DbSet<ButceKaydi> ButceKayitlari { get; set; }
    }
}
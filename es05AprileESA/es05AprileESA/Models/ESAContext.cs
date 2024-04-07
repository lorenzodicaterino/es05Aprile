using Microsoft.EntityFrameworkCore;

namespace es05AprileESA.Models
{
    public class ESAContext : DbContext
    {
        public ESAContext(DbContextOptions<ESAContext> options) : base(options)
            {

            }
        
        public DbSet<Oggetto> Oggettos { get; set; }
        public DbSet<Sistema> Sistemas { get; set; }
        public DbSet<OggettoSistema> OggettoSistemas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OggettoSistema>().HasKey(x => new { x.SistemaIdRiferimento, x.OggettoIdRiferimento });
            modelBuilder.Entity<OggettoSistema>().HasOne(i => i.OggettoRif).WithMany(q => q.OggettiSistema).HasForeignKey(i => i.OggettoIdRiferimento);
            modelBuilder.Entity<OggettoSistema>().HasOne(i => i.SistemaRif).WithMany(q => q.OggettiSistema).HasForeignKey(i => i.SistemaIdRiferimento);
        }
    }
}


using Microsoft.EntityFrameworkCore;

namespace patioAPI.Models
{
    public class PatioContext : DbContext
    {
        public PatioContext(DbContextOptions<PatioContext> options) : base(options) { }

        public DbSet<Moto> Motos { get; set; }
        public DbSet<Patio> Patios { get; set; }
        public DbSet<Filial> Filiais { get; set; }
        public DbSet<VeiculoPatio> VeiculoPatios { get; set; }
    }
}

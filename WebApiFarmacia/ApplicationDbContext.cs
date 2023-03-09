using Microsoft.EntityFrameworkCore;
using WebApiFarmacia.Entidades;

namespace WebApiFarmacia
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Medicamento> Medicamento { get; set;}
        public DbSet<Laboratorio> Laboratorio { get; set; }
        public DbSet<ModoAdministracion> ModoAdministracion { get; set; }
        public DbSet<Trabajador> Trabajador { get; set; }
        public DbSet<Venta> Venta { get; set; }
    }
}

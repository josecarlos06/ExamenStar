using ExamenWebStar.Models;
using Microsoft.EntityFrameworkCore;

namespace ExamenWebStar.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {}
        public DbSet<AreaModel> Area { get; set; }
        public DbSet<EmpleadoModel> Empleado { get; set; }

        public DbSet<AreaEmpleadoDto> AreaEmpleadoDtos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AreaEmpleadoDto>().HasNoKey();
            modelBuilder.Entity<EmpleadoModelDtos>().HasNoKey();
        }
    }
}

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
    }
}

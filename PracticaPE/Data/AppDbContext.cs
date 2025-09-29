using Microsoft.EntityFrameworkCore;
using PracticaPE.Models;

namespace PracticaPE.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<Meal> Meals { get; set; }  // <--- NUEVA TABLA
    }
}
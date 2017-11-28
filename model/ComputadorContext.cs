using Microsoft.EntityFrameworkCore;

namespace restApi.Model {
    public class ComputadorContext : DbContext {
        public ComputadorContext(DbContextOptions<ComputadorContext> options) : base(options) {
        }

        public DbSet<Computador> Computadores { get; set; }
    }
}
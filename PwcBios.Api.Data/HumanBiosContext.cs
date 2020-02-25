using Microsoft.EntityFrameworkCore;

namespace PwcBios.Api.Data
{
    public partial class HumanBiosContext : DbContext
    {
        public HumanBiosContext()
        {
        }

        public HumanBiosContext(DbContextOptions<HumanBiosContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //optionsBuilder.UseSqlServer("Server=localhost,1435;Database=HumanBios;User ID=sa;Password=j9r0uLEJK8!");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

using Microsoft.EntityFrameworkCore;

namespace RpsGame_NoDb
{
    public class RpsDbContext : DbContext
    {
        public DbSet<Player> players { get; set; }
        public DbSet<Round> rounds { get; set; }
        public DbSet<Match> matches { get; set; }

        public RpsDbContext(){}
        public RpsDbContext(DbContextOptions options) : base(options) {  }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            if(!options.IsConfigured)
            {
                options.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=RpsGame12142020;Trusted_Connection=True;");
            }
            
        }
    }
}                      
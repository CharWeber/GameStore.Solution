using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Models
{
  public class GameStoreContext : IdentityDbContext<ApplicationUser>
  {
    public DbSet<Game> Games { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<GameUser> GameUsers { get; set; }

    public GameStoreContext(DbContextOptions options) : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      optionsBuilder.UseLazyLoadingProxies();
    }
  }
}
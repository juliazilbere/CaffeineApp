
using Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Web.Data;

public class ApplicationDbContext : IdentityDbContext<AppUser, AppRole, long, IdentityUserClaim<long>, AppUserRole, IdentityUserLogin<long>, IdentityRoleClaim<long>, IdentityUserToken<long>>
{
  public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
      : base(options)
  {
  }

  public DbSet<Drink> Drinks { get; set; }
  public DbSet<DrinkLog> DrinkLog { get; set; }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);
    modelBuilder.Entity<CaffeineIntakeByUserResult>().HasNoKey().ToView(null);
  }
  public async Task<List<CaffeineIntakeByUserResult>> GetCaffeineIntakeByUser(long userId)
  {
    return await Set<CaffeineIntakeByUserResult>()
      .FromSqlRaw("EXEC CaffeineIntakeByUser @UserID={0}", userId)
      .ToListAsync();
  }
}


[Keyless]
public class CaffeineIntakeByUserResult
{
  public long UserId { get; set; }
  public string UserName { get; set; }
  public string Dt { get; set; }
  public int Hrs { get; set; }
  public double CaffeineIntakeNum { get; set; }
}
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Web.Data;

public class DrinkConfiguration : IEntityTypeConfiguration<Drink>
{
  public void Configure(EntityTypeBuilder<Drink> builder)
  {
    builder.HasData(new Drink() { Id = 1, Name = "Capucino", CafeinePer100ml = 150F, DefaultSize = 10F });
  }
}

using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;
using System.ComponentModel.DataAnnotations;

namespace Web.Data;

[Index(nameof(Name), nameof(UserId), IsUnique = true, Name = "IX_NAME_USER")]
public class Drink
{
  public int Id { get; set; }
  public string Name { get; set; }
  public double CafeinePer100ml { get; set; }
  public double DefaultSize { get;set; }
  public long? UserId { get; set; }
  public AppUser? User { get; set; }

  public DrinkOwnership Ownership => UserId.HasValue ? DrinkOwnership.Own : DrinkOwnership.Shared;

  
}

public enum DrinkOwnership
{
    Shared,
    Own
}
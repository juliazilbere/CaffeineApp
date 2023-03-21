using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Web.Data;

public class DrinkLog
{
  public int Id { get; set; }
  public int DrinkId { get; set; }
  public Drink? Drink { get; set; }
  public long UserId { get; set;}
  public AppUser? User { get; set; }
  public double ActualSize { get;set; }
  public DateTimeOffset DrinkTime { get; set; }
}


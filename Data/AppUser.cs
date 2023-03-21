using Microsoft.AspNetCore.Identity;

namespace Web.Data;

public class AppUser : IdentityUser<long>
{
    public List<Drink> Drinks { get; set; }
    public List<DrinkLog> DrinkLogs { get; set; }
}

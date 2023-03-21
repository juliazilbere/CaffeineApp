using Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Web.Data;

namespace Web.Pages.Reports
{
  public class Test2Model : PageModel
  {
    private readonly ApplicationDbContext _context;

    public Test2Model(Web.Data.ApplicationDbContext context)
    {
      _context = context;
    }

    public List<CaffeineIntakeByUserResult> Intakes { get; private set; }

    public async Task OnGetAsync()
    {
      try
      {
        var userId = User.GetUserId();
        Intakes = await _context.GetCaffeineIntakeByUser(userId);// .CaffeineIntakes.FromSqlRaw("EXEC CaffeineIntakeByUser @UserID = {0}", userId).ToListAsync();
        var b = 3;
      }
      catch (Exception ex)
      {

        throw;
      }

    }
  }
}

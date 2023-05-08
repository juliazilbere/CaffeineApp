using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Web.Data;

namespace Web.Pages.DrinkLogs
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly Web.Data.ApplicationDbContext _context;

        public IndexModel(Web.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<DrinkLog> DrinkLog { get;set; } = default!;

        public async Task OnGetAsync()
        {
            var userId = User.GetUserId();

            if (_context.DrinkLog != null)
            {
                DrinkLog = await _context.DrinkLog
                    .Where(x => x.UserId == userId)
                    .OrderByDescending(d => d.DrinkTime)
                    .Include(d => d.Drink)
                    .Include(d => d.User).ToListAsync();
            }
        }
    }
}

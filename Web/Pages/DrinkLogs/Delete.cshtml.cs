using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Web.Data;

namespace Web.Pages.DrinkLogs
{
    public class DeleteModel : PageModel
    {
        private readonly Web.Data.ApplicationDbContext _context;

        public DeleteModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
      public DrinkLog DrinkLog { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.DrinkLog == null)
            {
                return NotFound();
            }

            //var drinklog = await _context.DrinkLog.FirstOrDefaultAsync(m => m.Id == id);
            var drinklog = await _context.DrinkLog
                .Where(m => m.Id == id)
                .Include(d => d.Drink)
                .Include(d => d.User).ToListAsync();

            if (drinklog == null || drinklog.Count < 1)
            {
                return NotFound();
            }
            else 
            {
                DrinkLog = drinklog[0];
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.DrinkLog == null)
            {
                return NotFound();
            }
            var drinklog = await _context.DrinkLog.FindAsync(id);

            if (drinklog != null)
            {
                DrinkLog = drinklog;
                _context.DrinkLog.Remove(DrinkLog);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}

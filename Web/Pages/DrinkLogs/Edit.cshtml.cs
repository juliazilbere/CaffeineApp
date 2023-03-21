using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Web.Data;

namespace Web.Pages.DrinkLogs
{
    public class EditModel : PageModel
    {
        private readonly Web.Data.ApplicationDbContext _context;

        public EditModel(Web.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public DrinkLog DrinkLog { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.DrinkLog == null)
            {
                return NotFound();
            }

            var drinklog =  await _context.DrinkLog.FirstOrDefaultAsync(m => m.Id == id);
            if (drinklog == null)
            {
                return NotFound();
            }
            DrinkLog = drinklog;
            var userId = User.GetUserId();
            ViewData["DrinkId"] = new SelectList(_context.Drinks.Where(x => x.UserId == null || x.UserId == userId), "Id", "Name");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(DrinkLog).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DrinkLogExists(DrinkLog.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool DrinkLogExists(int id)
        {
          return _context.DrinkLog.Any(e => e.Id == id);
        }
    }
}

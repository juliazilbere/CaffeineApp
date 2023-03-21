using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Web.Data;

namespace WebApplication2.Pages.Drinks
{
    public class DeleteModel : PageModel
    {
        private readonly Web.Data.ApplicationDbContext _context;

        public DeleteModel(Web.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Drink Drink { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Drinks == null)
            {
                return NotFound();
            }

            var drink = await _context.Drinks.FirstOrDefaultAsync(m => m.Id == id);

            if (drink == null)
            {
                return NotFound();
            }
            else 
            {
                Drink = drink;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Drinks == null)
            {
                return NotFound();
            }
            var drink = await _context.Drinks.FindAsync(id);

            if (drink != null)
            {
                Drink = drink;
                _context.Drinks.Remove(Drink);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}

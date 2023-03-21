using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Web.Data;

namespace WebApplication2.Pages.Drinks
{
    public class EditModel : PageModel
    {
        private readonly Web.Data.ApplicationDbContext _context;

        public EditModel(Web.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Drink Drink { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Drinks == null)
            {
                return NotFound();
            }

            var drink =  await _context.Drinks.FirstOrDefaultAsync(m => m.Id == id);
            if (drink == null)
            {
                return NotFound();
            }
            Drink = drink;
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

            _context.Attach(Drink).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DrinkExists(Drink.Id))
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

        private bool DrinkExists(int id)
        {
          return _context.Drinks.Any(e => e.Id == id);
        }
    }
}

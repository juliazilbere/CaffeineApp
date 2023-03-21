using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Web.Data;

namespace Web.Pages.DrinkLogs
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CreateModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            var userId = User.GetUserId();
            ViewData["DrinkId"] = new SelectList(_context.Drinks.Where(x => x.UserId == null || x.UserId == userId), "Id", "Name");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return Page();
        }

        [BindProperty]
        public DrinkLog DrinkLog { get; set; }
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            var userId = User.GetUserId();
            DrinkLog.UserId = userId;
            DrinkLog.DrinkTime = DateTimeOffset.UtcNow;

            if (!ModelState.IsValid)
            {
                return Page();
            }
            
            _context.DrinkLog.Add(DrinkLog);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Web;
using Web.Data;

namespace WebApplication2.Pages.Drinks;

[Authorize]
public class CreateModel : PageModel
{
    private readonly Web.Data.ApplicationDbContext _context;

    public CreateModel(Web.Data.ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult OnGet()
    {
        return Page();
    }

    [BindProperty]
    public Drink Drink { get; set; }


    // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        var userId = User.GetUserId();
        Drink.UserId = userId;

        _context.Drinks.Add(Drink);
        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}

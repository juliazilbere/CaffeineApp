using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Web;
using Web.Data;

namespace WebApplication2.Pages.Drinks;



[Authorize]
public class IndexModel : PageModel
{
    private readonly Web.Data.ApplicationDbContext _context;

    public IndexModel(Web.Data.ApplicationDbContext context)
    {
        _context = context;
    }

    public IList<Drink> Drink { get;set; } = default!;

    public async Task OnGetAsync()
    {
        var userId = User.GetUserId();

        if (_context.Drinks != null)
        {
            //Drink = await _context.Drinks.ToListAsync();
            /**/
            var filteredDrinks = await _context.Drinks
                .Where(x=> x.UserId == null || x.UserId == userId)
                .ToListAsync();
            Drink = filteredDrinks;
        }
    }
}

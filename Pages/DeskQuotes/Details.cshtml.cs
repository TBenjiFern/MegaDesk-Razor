using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MegaDesk.Models;
using MegaDesk.Data;

namespace MegaDesk.Pages.DeskQuotes
{
    public class DetailsModel : PageModel
    {
        private readonly MegaDeskContext _context;

        public DetailsModel(MegaDeskContext context)
        {
            _context = context;
        }

      public DeskQuote DeskQuote { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.DeskQuote == null)
            {
                return NotFound();
            }

            var deskquote = await _context.DeskQuote.Include(d => d.DeliveryType).Include(d => d.Desk).Include(d => d.Desk.DesktopMaterial).FirstOrDefaultAsync(m => m.DeskQuoteId == id);
            if (deskquote == null)
            {
                return NotFound();
            }
            else 
            {
                DeskQuote = deskquote;
            }
            return Page();
        }
    }
}

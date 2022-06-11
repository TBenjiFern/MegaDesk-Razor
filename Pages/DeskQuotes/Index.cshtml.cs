using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MegaDesk.Models;
using MegaDesk.Data;

namespace MegaDesk.Pages.DeskQuotes
{
    public class IndexModel : PageModel
    {
        private readonly MegaDeskContext _context;

        public IndexModel(MegaDeskContext context)
        {
            _context = context;
        }

        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }

        public string CustomerNameSort { get; set; }
        public string DateSort { get; set; }
        public string CurrentSort { get; set; }

        public IList<DeskQuote> DeskQuote { get;set; } = default!;

        public async Task OnGetAsync(string sortOrder)
        {
            if (_context.DeskQuote != null)
            {
                DeskQuote = await _context.DeskQuote
                .Include(d => d.DeliveryType)
                .Include(d => d.Desk.DesktopMaterial)
                .Include(d => d.Desk).ToListAsync();
            }

            CustomerNameSort = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            DateSort = sortOrder == "Date" ? "date_desc" : "Date";

            var DeskQuoteOrdering = from d in _context.DeskQuote
                                    select d;
            
            switch (sortOrder)
            {
                case "name_desc":
                    DeskQuoteOrdering = DeskQuoteOrdering.OrderByDescending(d => d.CustomerName);
                    break;
                case "Date":
                    DeskQuoteOrdering = DeskQuoteOrdering.OrderBy(d => d.QuoteDate);
                    break;
                case "date_desc":
                    DeskQuoteOrdering = DeskQuoteOrdering.OrderByDescending(d => d.QuoteDate);
                    break;
                default:
                   DeskQuoteOrdering = DeskQuoteOrdering.OrderBy(d => d.CustomerName);
                    break;
            }

            if (!string.IsNullOrEmpty(SearchString))
            {
                DeskQuoteOrdering = DeskQuoteOrdering.Where(s => s.CustomerName.Contains(SearchString));
            }

            DeskQuote = await DeskQuoteOrdering.ToListAsync();
        }
    }
}

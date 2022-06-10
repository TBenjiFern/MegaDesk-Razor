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
    public class EditModel : PageModel
    {
        private readonly MegaDeskContext _context;

        public EditModel(MegaDeskContext context)
        {
            _context = context;
        }

        [BindProperty]
        public DeskQuote DeskQuote { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.DeskQuote == null)
            {
                return NotFound();
            }

            var deskquote = await _context.DeskQuote
                                .Include( d => d.DeliveryType)
                                .Include(d => d.Desk)
                                .FirstOrDefaultAsync(m => m.DeskQuoteId == id);
            if (deskquote == null)
            {
                return NotFound();
            }
            DeskQuote = deskquote;
            ViewData["DeliveryTypeId"] = new SelectList(_context.DeliveryType, "DeliveryTypeId", "DeliveryName");
            ViewData["DesktopMaterialId"] = new SelectList(_context.DesktopMaterial, "DesktopMaterialId", "DesktopMaterialName");

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

            _context.Attach(DeskQuote.Desk).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();

                DeskQuote.QuotePrice = DeskQuote.CalcTotalPrice(_context);

                DeskQuote.QuoteDate = DateTime.Now;

                _context.Attach(DeskQuote).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DeskQuoteExists(DeskQuote.DeskQuoteId))
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

        private bool DeskQuoteExists(int id)
        {
            return (_context.DeskQuote?.Any(e => e.DeskQuoteId == id)).GetValueOrDefault();
        }
    }
}

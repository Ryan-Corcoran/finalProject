using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CorkyID.Data;
using CorkyID.Models;
using System.Security.Claims;

namespace CorkyID
{
    public class DiscountEditModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DiscountEditModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Discount Discount { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Discount = _context.GetDiscount(id);

            if (Discount == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Discount.UserID = Guid.Parse(this.User.FindFirstValue(ClaimTypes.NameIdentifier));
            Discount.LastUpdated = DateTime.UtcNow;
            _context.Attach(Discount).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync(true);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!DiscountExists(Discount.DiscountID))
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


        private bool DiscountExists(Guid id)
        {
            return _context.Discount.Any(e => e.DiscountID == id);
        }
    }
}

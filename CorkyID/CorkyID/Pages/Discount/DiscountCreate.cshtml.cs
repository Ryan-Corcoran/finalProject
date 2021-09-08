using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using CorkyID.Data;
using CorkyID.Models;
using System.Security.Claims;

namespace CorkyID
{
    public class DiscountCreateModel : PageModel
    {
        private readonly CorkyID.Data.ApplicationDbContext _context;

        public DiscountCreateModel(CorkyID.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Discount Discount { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Discount.UserID = Guid.Parse(this.User.FindFirstValue(ClaimTypes.NameIdentifier));
            Discount.LastUpdated = DateTime.UtcNow;
            _context.Discount.Add(Discount);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}

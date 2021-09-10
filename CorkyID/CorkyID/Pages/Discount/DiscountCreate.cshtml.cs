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
using Microsoft.AspNetCore.Http;

namespace CorkyID
{
    public class DiscountCreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DiscountCreateModel(ApplicationDbContext context)
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

            try
            {
                Discount.UserID = Guid.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
                Discount.LastUpdated = DateTime.UtcNow;
                await _context.AddDiscountAsync(Discount);
            }
            catch (Exception)
            {
               ModelState.AddModelError("Error", "An error occured adding the new discount");
               return RedirectToPage("./Index");
            }

            return RedirectToPage("./Index");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using CorkyID.Data;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using CorkyID.Models;

namespace CorkyID
{
    public class SchemeCreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public SchemeCreateModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            if (User.Identity.IsAuthenticated)
            {
                return Page();
            } 
            else
            {
                return Redirect("./index");
            }
        }

        [BindProperty]
        public Schemes Schemes { get; set; }
        
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            Schemes.OwnerID = Guid.Parse(this.User.FindFirstValue(ClaimTypes.NameIdentifier));
            _context.Schemes.Add(Schemes);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}

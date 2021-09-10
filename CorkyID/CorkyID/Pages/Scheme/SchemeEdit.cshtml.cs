using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using CorkyID.Data;
using CorkyID.Models;

namespace CorkyID
{
    public class SchemeEditModel : PageModel
    {
        private readonly CorkyID.Data.ApplicationDbContext _context;

        public SchemeEditModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Schemes Schemes { get; set; }

        [BindProperty]
        public IList<SchemeUsers> SchemeUsers { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Schemes = await _context.Schemes.FirstOrDefaultAsync(m => m.SchemeID == id);

            if (Schemes == null)
            {
                return NotFound();
            }

            SchemeUsers = await _context.SchemeUsers.Where(x => x.SchemeID == id).ToListAsync();



            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Schemes.OwnerID = Guid.Parse(this.User.FindFirstValue(ClaimTypes.NameIdentifier));
            _context.Attach(Schemes).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SchemesExists(Schemes.SchemeID))
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

        private bool SchemesExists(Guid id)
        {
            return _context.Schemes.Any(e => e.SchemeID == id);
        }
    }
}

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

namespace CorkyID
{
    public class EditModel : PageModel
    {
        private readonly CorkyID.Data.ApplicationDbContext _context;

        public EditModel(CorkyID.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Schemes Schemes { get; set; }

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
            return Page();
        }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CorkyID.Data;
using CorkyID.Models;


namespace CorkyID
{
    public class DetailsModel : PageModel
    {
        private readonly CorkyID.Data.ApplicationDbContext _context;

        public DetailsModel(CorkyID.Data.ApplicationDbContext context)
        {
            _context = context;
        }

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
            SchemeUsers = await _context.SchemeUsers.Where(x => x.SchemeID == id).ToListAsync();

            if (Schemes == null)
            {
                return NotFound();
            }
            return Page();
        }


        public string GetUserEmail (Guid Id)
        {
            var user = _context.Users.FirstOrDefaultAsync(x => x.Id == Id.ToString()).Result;
            return user.Email;
        }
    }
}

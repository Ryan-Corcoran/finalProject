using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CorkyID.Data;
using CorkyID.Models;
using System.Security.Claims;

namespace CorkyID
{
    public class SchemeDetailsModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public SchemeDetailsModel(ApplicationDbContext context)
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
        //public async Task<IActionResult> OnGetAddNewUser(string email)
        //{
        //    SchemeUsers SU = new SchemeUsers
        //    {
        //        UserID = Guid.Parse(this.User.FindFirstValue(email)),
        //        SchemeID = Guid.Parse("")
        //    };
        //    _context.Add(SU);
        //    await _context.SaveChangesAsync();
        //    return RedirectToPage();
        //}

        public string GetUserEmail (Guid Id)
        {
            var user = _context.Users.FirstOrDefaultAsync(x => x.Id == Id.ToString()).Result;
            return user.Email;
        }

        
    }
}

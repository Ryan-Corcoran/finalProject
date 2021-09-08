using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using CorkyID.Data;
using CorkyID.Models;

namespace CorkyID
{
    public class SchemeIndexModel : PageModel
    {
        private readonly CorkyID.Data.ApplicationDbContext _context;

        public SchemeIndexModel(CorkyID.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Schemes> Schemes { get;set; }
        public IList<SchemeUsers> SchemeUsers { get; set; }

        public async Task OnGetAsync()
        {
            Schemes = await _context.Schemes.Where(x => x.ValidToDate >= DateTime.Now).ToListAsync();
        }

        public async Task<IActionResult> OnGetUnassignUser(string Id)
        {
            var UserID = Guid.Parse(this.User.FindFirstValue(ClaimTypes.NameIdentifier));
            SchemeUsers = await _context.SchemeUsers.Where(x => x.UserID == (UserID) && x.SchemeID == Guid.Parse(Id)).ToListAsync();

            SchemeUsers SU = new SchemeUsers();
            SU = SchemeUsers[0];
            _context.SchemeUsers.Remove(SU);
            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");
        }

        public async Task<IActionResult> OnGetAssignUser(string Id)
        {
            SchemeUsers SU = new SchemeUsers
            {
                UserID = Guid.Parse(this.User.FindFirstValue(ClaimTypes.NameIdentifier)),
                SchemeID = Guid.Parse(Id)
            };
            _context.Add(SU);
            await _context.SaveChangesAsync();
            return RedirectToPage();
        }

        public async Task<Boolean> IsUserEnrolled(string userID, Guid schemeID)
        {
            SchemeUsers = await _context.SchemeUsers.Where(x => x.UserID == Guid.Parse(userID) && x.SchemeID == schemeID).ToListAsync();
            if (SchemeUsers.Count != 0)
            {
                return true;
            } else
            {
                return false;
            }
        }
    }
}

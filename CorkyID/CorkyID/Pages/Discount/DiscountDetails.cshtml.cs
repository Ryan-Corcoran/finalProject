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
    public class DiscountDetailsModel : PageModel
    {
        private readonly CorkyID.Data.ApplicationDbContext _context;

        public DiscountDetailsModel(CorkyID.Data.ApplicationDbContext context)
        {
            _context = context;
        }

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

        public async Task<Boolean> IsUserOwner(string userID, Guid discountID)
        {
            var discountOwnerList = await _context.Discount.Where(x => x.UserID == Guid.Parse(userID) && x.DiscountID == discountID).ToListAsync();
            if (discountOwnerList.Count != 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}

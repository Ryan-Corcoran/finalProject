using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using CorkyID.Data;
using Microsoft.EntityFrameworkCore;

namespace CorkyID.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly ApplicationDbContext _context;
        public IList<Models.Discount> Discounts { get; set; }

        public IList<Models.Schemes> Schemes { get; set; }

        public IList<Models.SchemeUsers> SchemeUsers { get; set; }

        public IndexModel(ILogger<IndexModel> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }
        
        public async Task OnGet()
        {
            Discounts = await _context.Discount.Where(x => x.ValidTo >= DateTime.UtcNow).ToListAsync();
        }

        public Boolean DisplayDiscount(string userID, Guid discountID)
        {
            var discount = _context.GetDiscount(discountID);
            var schemeRestriction = discount.DiscountViewRestrictions;
            if (schemeRestriction == "All Schemes")
            {
                return true;
            }
            else
            {
            if (userID != null)
            {
                var schemes = _context.Schemes.First(x => x.Name == schemeRestriction).SchemeID.ToString();
                var found = _context.SchemeUsers.Any(x => x.UserID == Guid.Parse(userID) && x.SchemeID == Guid.Parse(schemes));
                return found;
            } else
                {
                    return false;
                }
            }
        }
    }
}

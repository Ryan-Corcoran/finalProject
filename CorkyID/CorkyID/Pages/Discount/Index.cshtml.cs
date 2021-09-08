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
    public class DiscountIndexModel : PageModel
    {
        private readonly CorkyID.Data.ApplicationDbContext _context;

        public DiscountIndexModel(CorkyID.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Discount> Discount { get;set; }

        public async Task OnGetAsync()
        {
            Discount = await _context.Discount.ToListAsync();
        }
    }
}

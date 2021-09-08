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

        public IndexModel(ILogger<IndexModel> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }
        
        public async Task OnGet()
        {
            Discounts = await _context.Discount.Where(x => x.ValidTo >= DateTime.UtcNow).ToListAsync();
        }
    }
}

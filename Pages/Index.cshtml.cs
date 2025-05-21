using ekzVar.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ekzVar.Models;
using Microsoft.EntityFrameworkCore;

namespace ekzVar.Pages
{
    public class IndexModel : PageModel
    {
        private readonly AppDbContext _context;

        public IndexModel(AppDbContext context)
        {
            _context = context;
        }

        public IList<Oboi>? Oboi { get; set; }

        public async Task OnGetAsync()
        {
            Oboi = await _context.Oboi
                .Include(o => o.OsnovaMaterial)
                .Include(o => o.PokritieMaterial)
                .ToListAsync();
        }
    }
}
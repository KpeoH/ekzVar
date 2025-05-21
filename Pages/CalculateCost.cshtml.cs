using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ekzVar.Models;
using ekzVar.Data;

namespace ekzVar.Pages
{
    public class CalculateCostModel : PageModel
    {
        private readonly AppDbContext _context;

        public CalculateCostModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }

        public Oboi Oboi { get; set; }

        public float TotalCost { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            Oboi = await _context.Oboi
                .Include(o => o.OsnovaMaterial)
                .Include(o => o.PokritieMaterial)
                .FirstOrDefaultAsync(m => m.Id == Id);

            if (Oboi == null)
            {
                return NotFound();
            }

            TotalCost = Oboi.OsnovaMaterial!.Price + Oboi.PokritieMaterial!.Price;

            return Page();
        }
    }
}
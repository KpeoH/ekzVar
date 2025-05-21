using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ekzVar.Data;
using ekzVar.Models;

namespace ekzVar.Pages
{
    public class CreateOboiModel : PageModel
    {
        private readonly ekzVar.Data.AppDbContext _context;

        public CreateOboiModel(ekzVar.Data.AppDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["OsnovaMaterialId"] = new SelectList(_context.Materials, "Id", "Name");
        ViewData["PokritieMaterialId"] = new SelectList(_context.Materials, "Id", "Name");
            return Page();
        }

        [BindProperty]
        public Oboi Oboi { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Oboi.Add(Oboi);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}

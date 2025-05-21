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
    public class CreateMaterialModel : PageModel
    {
        private readonly ekzVar.Data.AppDbContext _context;

        public CreateMaterialModel(ekzVar.Data.AppDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Materials Materials { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Materials.Add(Materials);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}

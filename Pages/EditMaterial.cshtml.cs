using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ekzVar.Data;
using ekzVar.Models;

namespace ekzVar.Pages
{
    public class EditMaterialModel : PageModel
    {
        private readonly ekzVar.Data.AppDbContext _context;

        public EditMaterialModel(ekzVar.Data.AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Materials Materials { get; set; } = default!;

        public float MaterialOldPrice { get; set; }
        public string MaterialOldName { get; set; }
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var materials =  await _context.Materials.FirstOrDefaultAsync(m => m.Id == id);
            if (materials == null)
            {
                return NotFound();
            }
            Materials = materials;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            var originalMaterial = await _context.Materials.AsNoTracking().FirstOrDefaultAsync(m => m.Id == Materials.Id);
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Materials).State = EntityState.Modified;

            MaterialOldPrice = originalMaterial!.Price;
            MaterialOldName = originalMaterial!.Name;

            try
            {
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = $"Материал под номером {Materials.Id} был успешно изменён!" +
                    $"\nСтарое название: {MaterialOldName} | Новое название: {Materials.Name}" +
                    $"\nСтарая стоимость: {MaterialOldPrice} | Новая стоимость: {Materials.Price}";
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MaterialsExists(Materials.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            
            return RedirectToPage("./Materials");
        }

        private bool MaterialsExists(int id)
        {
            return _context.Materials.Any(e => e.Id == id);
        }
    }
}

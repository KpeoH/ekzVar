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
    public class EditModel : PageModel
    {
        private readonly ekzVar.Data.AppDbContext _context;

        public EditModel(ekzVar.Data.AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Oboi Oboi { get; set; } = default!;

        public string OldOboiName {  get; set; }
        public string OldOsnovaName { get; set; }
        public string OldPokritieName { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }


            var oboi =  await _context.Oboi
                .Include(o => o.OsnovaMaterial)
                .Include(o => o.PokritieMaterial)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (oboi == null)
            {
                return NotFound();
            }
            Oboi = oboi;
           ViewData["OsnovaMaterialId"] = new SelectList(_context.Materials, "Id", "Name");
           ViewData["PokritieMaterialId"] = new SelectList(_context.Materials, "Id", "Name");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            var oboiOld = await _context.Oboi
                .Include(o => o.OsnovaMaterial)
                .Include(o => o.PokritieMaterial)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == Oboi.Id);

            if (!ModelState.IsValid)
            {
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine(error.ErrorMessage);
                }
                return Page();
            }

            _context.Attach(Oboi).State = EntityState.Modified;
            OldOboiName = oboiOld!.Name;
            OldOsnovaName = oboiOld.OsnovaMaterial!.Name;
            OldPokritieName = oboiOld.PokritieMaterial!.Name;
            try
            {
                await _context.SaveChangesAsync();

                var UpdatedOboi = await _context.Oboi
                    .Include(o => o.OsnovaMaterial)
                    .Include(o => o.PokritieMaterial)
                    .AsNoTracking()
                    .FirstOrDefaultAsync(m => m.Id== Oboi.Id);

                TempData["SuccessMessage"] = $"Обои успешно изменены!<br><br>" +
                    $"Старое название: {OldOboiName} | Новое название: {UpdatedOboi!.Name}<br>" +
                    $"Старая основа: {OldOsnovaName} | Новая основа: {UpdatedOboi!.OsnovaMaterial!.Name}<br>" +
                    $"Старое покрытие: {OldPokritieName} | Новое покрытие: {UpdatedOboi!.PokritieMaterial!.Name}";
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OboiExists(Oboi.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool OboiExists(int id)
        {
            return _context.Oboi.Any(e => e.Id == id);
        }
    }
}

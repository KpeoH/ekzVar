using ekzVar.Data;
using ekzVar.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ekzVar.Pages
{
    public class MaterialsModel : PageModel
    {
        private readonly AppDbContext _context;

        public MaterialsModel(AppDbContext context)
        {
            _context = context;
        }

        public IList<Materials>? Materials { get; set; } 

        public async Task OnGetAsync()
        {
            Materials = await _context.Materials.ToListAsync();
        }
    }

}

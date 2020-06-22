using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Teacher;

namespace SchoolDB1.Models
{
    public class DetailsModel : PageModel
    {
        private readonly SchoolDB1.Models.SchoolDB1Context _context;

        public DetailsModel(SchoolDB1.Models.SchoolDB1Context context)
        {
            _context = context;
        }

        public Teachers Teacher { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Teacher = await _context.Teachers.FirstOrDefaultAsync(m => m.Id == id);

            if (Teacher == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}

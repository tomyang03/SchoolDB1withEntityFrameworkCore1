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
    public class IndexModel : PageModel
    {
        private readonly SchoolDB1.Models.SchoolDB1Context _context;

        public IndexModel(SchoolDB1.Models.SchoolDB1Context context)
        {
            _context = context;
        }

        public IList<Teachers> Teacher { get;set; }

        public async Task OnGetAsync()
        {
            Teacher = await _context.Teachers.ToListAsync();
        }
    }
}

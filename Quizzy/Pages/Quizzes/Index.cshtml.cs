using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Quizzy.Data;
using Quizzy.Models;

namespace Quizzy.Pages.Quizzes
{
    public class IndexModel : PageModel
    {
        private readonly Quizzy.Data.QuizzyContext _context;

        public IndexModel(Quizzy.Data.QuizzyContext context)
        {
            _context = context;
        }

        public IList<Quiz> Quiz { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Quiz = await _context.Quizzes
                .Include(q => q.Assignments)
                .ThenInclude(a => a.Question)
                .ToListAsync();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Quizzy.Data;
using Quizzy.Models;

namespace Quizzy.Pages.Questions
{
    public class DetailsModel : PageModel
    {
        private readonly Quizzy.Data.QuizzyContext _context;

        public DetailsModel(Quizzy.Data.QuizzyContext context)
        {
            _context = context;
        }

        public Question Question { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // var question = await _context.Questions.FirstOrDefaultAsync(m => m.Id == id);
            var question = await _context.Questions
                .Include(q => q.Assignments)
                .ThenInclude(a => a.Quiz)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);
            
            if (question == null)
            {
                return NotFound();
            }
            else
            {
                Question = question;
            }
            return Page();
        }
    }
}

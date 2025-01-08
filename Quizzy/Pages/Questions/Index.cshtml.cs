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
    public class IndexModel : PageModel
    {
        private readonly Quizzy.Data.QuizzyContext _context;

        public IndexModel(Quizzy.Data.QuizzyContext context)
        {
            _context = context;
        }

        public IList<Question> Question { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Question = await _context.Questions.ToListAsync();
        }
    }
}

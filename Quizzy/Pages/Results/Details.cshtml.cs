using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Quizzy.Models;
using System.Threading.Tasks;
using Quizzy.Data;

namespace Quizzy.Pages.Results
{
    public class DetailsModel : PageModel
    {
        private readonly QuizzyContext _context;

        public DetailsModel(QuizzyContext context)
        {
            _context = context;
        }

        public Attempt Attempt { get; set; }
        public string QuizTitle { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var attempt = await _context.Attempts.FirstOrDefaultAsync(a => a.AttemptId == id);

            if (attempt == null)
            {
                return NotFound();
            }
            
            Attempt = attempt;
            var quiz = await _context.Quizzes.FindAsync(Attempt.QuizId);
            QuizTitle = quiz?.Title ?? "Unknown Quiz";

            return Page();
        }
    }
}
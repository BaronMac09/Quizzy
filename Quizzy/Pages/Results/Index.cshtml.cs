using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Quizzy.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Quizzy.Data;

namespace Quizzy.Pages.Results
{
    public class IndexModel : PageModel
    {
        private readonly QuizzyContext _context;

        public IndexModel(QuizzyContext context)
        {
            _context = context;
        }

        public IList<Attempt> Attempts { get; set; }
        public Dictionary<int, string> QuizTitles { get; set; } = new Dictionary<int, string>();

        public async Task OnGetAsync()
        {
            // Fetch all attempts in the database
            var allAttempts = await _context.Attempts.ToListAsync();

            // Fetch all quizzes existing in the database
            var validQuizIds = await _context.Quizzes.Select(q => q.QuizId).ToListAsync();

            // Remove attempts that reference non-existent quizzes
            var unreferencedAttempts = allAttempts.Where(a => !validQuizIds.Contains(a.QuizId)).ToList();
            if (unreferencedAttempts.Count != 0)
            {
                _context.Attempts.RemoveRange(unreferencedAttempts);
                await _context.SaveChangesAsync();
            }

            // Refresh Attempts after update for consistency
            Attempts = await _context.Attempts.ToListAsync();

            // Fetch quiz titles for existing quizzes
            var quizzes = await _context.Quizzes.ToDictionaryAsync(q => q.QuizId, q => q.Title);

            // Populate QuizTitles while avoiding duplicate keys
            QuizTitles = new Dictionary<int, string>();
            foreach (var attempt in Attempts)
            {
                if (!QuizTitles.ContainsKey(attempt.QuizId))
                {
                    QuizTitles[attempt.QuizId] = quizzes.ContainsKey(attempt.QuizId)
                        ? quizzes[attempt.QuizId]
                        : "Unknown Quiz";
                }
            }
        }
    }
}
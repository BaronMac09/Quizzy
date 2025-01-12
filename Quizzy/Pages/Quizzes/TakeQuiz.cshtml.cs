using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Quizzy.Models;
using Quizzy.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quizzy.Pages.Quizzes
{
    public class TakeQuizModel : PageModel
    {
        private readonly QuizzyContext _context;

        public TakeQuizModel(QuizzyContext context)
        {
            _context = context;
        }

        public Quiz Quiz { get; set; }

        [BindProperty]
        public Dictionary<int, string> UserAnswers { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var quiz = await _context.Quizzes
                .Include(q => q.Assignments)
                .ThenInclude(a => a.Question)
                .FirstOrDefaultAsync(q => q.QuizId == id);

            if (quiz == null)
            {
                return NotFound();
            }
            
            Quiz = quiz;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var quiz = await _context.Quizzes
                .Include(q => q.Assignments)
                .ThenInclude(a => a.Question)
                .FirstOrDefaultAsync(q => q.QuizId == id);

            if (quiz == null)
            {
                return NotFound();
            }

            // Calculate the score
            var score = 0f;
            var numQuestions = quiz.Assignments.Count;

            foreach (var assignment in quiz.Assignments)
            {
                var correctAnswer = assignment.Question.CorrectAnswer;
                if (UserAnswers.TryGetValue(assignment.QuestionId, out var userAnswer) && userAnswer == correctAnswer)
                {
                    score++;
                }
            }
            
            // Ensuring no exception for dividing by zero
            var finalScore = numQuestions > 0 ? (score / numQuestions) * 100 : 0;

            // Create the new attempt after user has taken the quiz
            var attempt = new Attempt
            {
                QuizId = quiz.QuizId,
                Score = finalScore,
                DateTaken = DateTime.Now
            };

            _context.Attempts.Add(attempt);
            await _context.SaveChangesAsync();

            // Redirect to the results page corresponding to the correct attempt ID
            return RedirectToPage("/Results/Details", new { id = attempt.AttemptId });
        }
    }
}
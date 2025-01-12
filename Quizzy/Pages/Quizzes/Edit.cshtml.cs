using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Quizzy.Data;
using Quizzy.Models;

namespace Quizzy.Pages.Quizzes
{
    public class EditModel : PageModel
    {
        private readonly Quizzy.Data.QuizzyContext _context;

        public EditModel(Quizzy.Data.QuizzyContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Quiz Quiz { get; set; } = default!;
        [BindProperty]
        public List<int> SelectedQuestionIds { get; set; } = new List<int>();
        public List<Question> Questions { get; set; } = new List<Question>();

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            var quiz = await _context.Quizzes
                .Include(q => q.Assignments)
                .ThenInclude(a => a.Question)
                .FirstOrDefaultAsync(m => m.QuizId == id);

            if (quiz == null)
            {
                return NotFound();
            }
            
            Quiz = quiz;

            Questions = await _context.Questions.ToListAsync();
            SelectedQuestionIds = Quiz.Assignments.Select(a => a.QuestionId).ToList();

            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page(); // Return the current page to display validation errors
            }

            var quizToUpdate = await _context.Quizzes
                .Include(q => q.Assignments)
                .FirstOrDefaultAsync(q => q.QuizId == Quiz.QuizId);

            if (quizToUpdate == null)
            {
                return NotFound();
            }

            quizToUpdate.Title = Quiz.Title;
            
            // Update the LastModified property
            quizToUpdate.LastModified = DateTime.Now;

            // Handle assignment updates (questions)
            var existingAssignments = quizToUpdate.Assignments.Select(a => a.QuestionId).ToList();
            var toAdd = SelectedQuestionIds.Except(existingAssignments).ToList();
            var toRemove = existingAssignments.Except(SelectedQuestionIds).ToList();

            foreach (var questionId in toAdd)
            {
                quizToUpdate.Assignments.Add(new Assignment { QuizId = quizToUpdate.QuizId, QuestionId = questionId });
            }

            foreach (var questionId in toRemove)
            {
                var assignment = quizToUpdate.Assignments.FirstOrDefault(a => a.QuestionId == questionId);
                if (assignment != null)
                {
                    _context.Assignments.Remove(assignment);
                }
            }
            
            await _context.SaveChangesAsync();

            // Redirect to the Index page after saving
            return RedirectToPage("./Index");
        }

        private bool QuizExists(int id)
        {
            return _context.Quizzes.Any(e => e.QuizId == id);
        }
    }
}

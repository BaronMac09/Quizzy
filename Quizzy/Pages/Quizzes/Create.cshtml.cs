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
    public class CreateModel : PageModel
    {
        private readonly Quizzy.Data.QuizzyContext _context;

        public CreateModel(Quizzy.Data.QuizzyContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Quiz Quiz { get; set; } 
        public List<Question> Questions { get; set; } = new List<Question>();
        
        [BindProperty]
        public List<int> SelectedQuestionIds { get; set; } = new List<int>();
        
        public async Task<IActionResult> OnGetAsync()
        {
            // Retrieve all questions from the database
            Questions = await _context.Questions.ToListAsync();
            return Page();
        }

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                // Reload questions if failed validation
                Questions = await _context.Questions.ToListAsync();
                return Page();
            }
            
            // Add the newly created quiz to the database
            Quiz.LastModified = DateTime.Now; // Set the LastModified property to the current date and time
            _context.Quizzes.Add(Quiz);
            await _context.SaveChangesAsync();
            
            // Generate new Assignments to link the selected questions to the quiz
            if (SelectedQuestionIds.Any())
            {
                var assignments = SelectedQuestionIds.Select(questionId => new Assignment
                {
                    QuizId = Quiz.QuizId,
                    QuestionId = questionId
                });
                
                _context.Assignments.AddRange(assignments);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}

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

namespace Quizzy.Pages.Questions
{
    public class EditModel : PageModel
    {
        private readonly Quizzy.Data.QuizzyContext _context;

        public EditModel(Quizzy.Data.QuizzyContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Question? Question { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Question =  await _context.Questions.FindAsync(id);
            if (Question == null)
            {
                return NotFound();
            }
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int id)
        {
            var questionToUpdate = await _context.Questions.FindAsync(id);
            
            if (questionToUpdate == null)
            {
                return NotFound();
            }

            if (!await TryUpdateModelAsync<Question>(
                    questionToUpdate,
                    "question",
                    q => q.Text, q => q.Answers, q => q.CorrectAnswer)) 
                return RedirectToPage("./Index");
            
            questionToUpdate.LastModified = DateTime.Now;
            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");
        }

        private bool QuestionExists(int id)
        {
            return _context.Questions.Any(e => e.Id == id);
        }
    }
}

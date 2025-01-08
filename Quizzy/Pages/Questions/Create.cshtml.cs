using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Quizzy.Data;
using Quizzy.Models;

namespace Quizzy.Pages.Questions
{
    public class CreateModel : PageModel
    {
        private readonly Quizzy.Data.QuizzyContext _context;
        public readonly int MaxAnswers = 6;

        public CreateModel(Quizzy.Data.QuizzyContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Question Question { get; set; } 

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            // Set the LastModified property to the current date and time
            Question.LastModified = DateTime.Now;
            
            if (!ModelState.IsValid)
            {
                return Page();
            }
            
            
            _context.Questions.Add(Question);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}

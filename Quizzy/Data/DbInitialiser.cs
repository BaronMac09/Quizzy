using Quizzy.Models;

namespace Quizzy.Data;

public static class DbInitialiser
{
    public static void Initialise(QuizzyContext context)
    {
        // Check if the database has been created
        if (context.Questions.Any())
        {
            return; // DB has been seeded
        }
        
        // Otherwise, create new questions
        var questions = new Question[]
        {
            new Question
            {
                Id = 1, QuizIds = [],
                Text = "What is the capital of France?", CorrectAnswer = "Paris",
                Answers = ["Paris", "London", "Berlin", "Madrid"], LastModified = DateTime.Now
            },
            new Question
            {
                Id = 2, QuizIds = [],
                Text = "What is the capital of Germany?", CorrectAnswer = "Berlin",
                Answers = ["Paris", "London", "Berlin", "Madrid"], LastModified = DateTime.Now
            },
            new Question
            {
                Id = 3, QuizIds = [],
                Text = "What is the capital of Spain?", CorrectAnswer = "Madrid",
                Answers = ["Paris", "London", "Berlin", "Madrid"], LastModified = DateTime.Now
            },
            new Question
            {
                Id = 4, QuizIds = [],
                Text = "What is the capital of the UK?", CorrectAnswer = "London",
                Answers = ["Paris", "London", "Berlin", "Madrid"], LastModified = DateTime.Now
            },
        };
        
        context.Questions.AddRange(questions);
        context.SaveChanges();
        
        var quizzes = new Quiz[]
        {
            new Quiz
            {
                Id = 1, Title = "Capitals of Europe", Questions = questions, LastModified = DateTime.Now
            }
        };
        
        context.Quizzes.AddRange(quizzes);
        context.SaveChanges();
    }
}
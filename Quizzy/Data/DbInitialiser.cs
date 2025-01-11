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
        var questions = new List<Question>
        {
            new Question
            {
                QuestionId = 1,
                Text = "What is the capital of France?", CorrectAnswer = "Paris",
                Answers = ["Paris", "London", "Berlin", "Madrid"], LastModified = DateTime.Now
            },
            new Question
            {
                QuestionId = 2, 
                Text = "What is the capital of Germany?", CorrectAnswer = "Berlin",
                Answers = ["Paris", "London", "Berlin", "Madrid"], LastModified = DateTime.Now
            },
            new Question
            {
                QuestionId = 3,
                Text = "What is the capital of Spain?", CorrectAnswer = "Madrid",
                Answers = ["Paris", "London", "Berlin", "Madrid"], LastModified = DateTime.Now
            },
            new Question
            {
                QuestionId = 4,
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
                QuizId = 1, Title = "Capitals of Europe", LastModified = DateTime.Now
            }
        };  
        
        context.Quizzes.AddRange(quizzes);
        context.SaveChanges();
        
        var assignments = new Assignment[]
        {
            new Assignment
            {
                AssignmentId = 1, QuizId = 1, QuestionId = 1
            },
            new Assignment
            {
                AssignmentId = 2, QuizId = 1, QuestionId = 2
            },
            new Assignment
            {
                AssignmentId = 3, QuizId = 1, QuestionId = 3
            },
            new Assignment
            {
                AssignmentId = 4, QuizId = 1, QuestionId = 4
            }
        };
        
        context.Assignments.AddRange(assignments);
        context.SaveChanges();
    }
}
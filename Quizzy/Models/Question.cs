namespace Quizzy.Models;

public class Question
{
    public int Id { get; set; }
    public string Text { get; set; }
    public List<string> Answers { get; set; }
    public string CorrectAnswer { get; set; }
    public DateTime LastModified { get; set; }
    
    public List<int> QuizIds { get; set; } // Many-to-Many relationship
}
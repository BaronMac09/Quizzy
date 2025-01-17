namespace Quizzy.Models;

public class Quiz
{
    public int QuizId { get; set; }
    public string Title { get; set; }
    public DateTime LastModified { get; set; }
    
    public ICollection<Assignment> Assignments { get; set; } = []; // One-to-many relationship
}
namespace Quizzy.Models;

public class Question
{
    public int Id { get; set; }
    public string Text { get; set; }
    public List<string> Answers { get; set; }
    public string CorrectAnswer { get; set; }
    public DateTime LastModified { get; set; }

    public ICollection<Assignment> Assignments { get; set; } = []; // One-to-many relationship
}
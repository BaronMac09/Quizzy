namespace Quizzy.Models;

public class Assignment
{
    public int AssignmentId { get; set; }
    public int QuizId { get; set; }
    public int QuestionId { get; set; }
    
    public Quiz Quiz { get; set; }
    public Question Question { get; set; }
}
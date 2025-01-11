namespace Quizzy.Models;

public class Attempt
{
    public int AttemptId { get; set; }
    public int QuizId { get; set; }
    public float Score { get; set; }
    public DateTime DateTaken { get; set; }
}
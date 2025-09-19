namespace VotingSystem.API.Models;

public class Vote
{
    public int Id { get; set; }
    public int AnswerOptionId { get; set; }
    public AnswerOption AnswerOption { get; set; }
    public DateTime Timestamp { get; set; } 
}
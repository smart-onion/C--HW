namespace VotingSystem.API.Models;

public class Question
{
    public int Id { get; set; }
    public string Text { get; set; }
    public int PollId { get; set; }
    public Poll Poll { get; set; }
    public List<AnswerOption> Options { get; set; }
}
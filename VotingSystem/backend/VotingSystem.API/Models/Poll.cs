namespace VotingSystem.API.Models;

public class Poll
{
    public int Id { get; set; }
    public string Title { get; set; }
    public List<Question> Questions { get; set; }
}

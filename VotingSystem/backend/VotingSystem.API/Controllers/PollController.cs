using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VotingSystem.API.Data;
using VotingSystem.API.Models;

namespace VotingSystem.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PollController : ControllerBase
{
    private readonly ApplicationContext _context;
 
    public PollController(ApplicationContext context)
    {
        _context = context;
    }
 
    [HttpGet]
    public IActionResult GetPolls()
    {
        var polls = _context.Polls
            .Select(p => new { p.Id, p.Title })
            .ToList();
        return Ok(polls);
    }
 
    [HttpGet("{id}")]
    public IActionResult GetPollDetails(int id)
    {
        var poll = _context.Polls
            .Where(p => p.Id == id)
            .Select(p => new {
                p.Id,
                p.Title,
                Questions = p.Questions.Select(q => new {
                    q.Id,
                    q.Text,
                    Options = q.Options.Select(o => new { o.Id, o.Text })
                })
            })
            .FirstOrDefault();
 
        if (poll == null)
            return NotFound();
 
        return Ok(poll);
    }
 
    [HttpPost("vote")]
    public async Task<IActionResult> SubmitVote([FromBody] List<int> answerOptionIds)
    {
        var votes = answerOptionIds.Select(id => new Vote { AnswerOptionId = id }).ToList();
        _context.Votes.AddRange(votes);
        await _context.SaveChangesAsync();
        return Ok();
    }
 
    [HttpGet("{id}/results")]
    public IActionResult GetResults(int id)
    {
        var poll = _context.Polls
            .Include(p => p.Questions)
                .ThenInclude(q => q.Options)
                .ThenInclude(o => o.Votes)
            .FirstOrDefault(p => p.Id == id);
 
        if (poll == null)
            return NotFound();
 
        var results = poll.Questions.Select(q => new {
            q.Text,
            Options = q.Options.Select(o => new {
                o.Text,
                Votes = o.Votes.Count,
                Percentage = o.Votes.Count == 0 ? 0 :
                                (double)o.Votes.Count / q.Options.Sum(opt => opt.Votes.Count) * 100
            })
        });
 
        return Ok(results);
    }
}
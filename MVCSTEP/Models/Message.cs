using System.ComponentModel.DataAnnotations;
using MimeKit;

namespace MVCSTEP.Models;

public class Message
{
    [EmailAddress]
    public string To { get; set; }
    public string? Subject { get; set; }
    public string? Body { get; set; }
    public TimeSpan? Delay { get; set; } 
}
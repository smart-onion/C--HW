using System.ComponentModel.DataAnnotations;

namespace MVCSTEP.ViewModels;

public class NoteViewModel
{
    public int UserId { get; set; }
    [Required] public string Subject { get; set; }
    public string Body { get; set; }
}
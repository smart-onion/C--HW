using System.ComponentModel.DataAnnotations;

namespace MVCSTEP.Models;

public class User
{
    public string Name { get; set; }
    public int Age { get; set; }
    public int Score { get; set; }
}
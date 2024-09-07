public class ProjectsTable
{
    public int Id { get; set; }
    public int ProjectId { get; set; }
    public int EmployeeId { get; set; }

    public Project Project { get; set; }
    public Employee Employee { get; set; }
}


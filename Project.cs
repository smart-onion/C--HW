public class Project
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime Deadline { get; set; }

    public List<ProjectsTable> ProjectsTable { get; set; }
}


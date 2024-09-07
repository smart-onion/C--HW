using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

internal class Program
{
    private static void Main(string[] args)
    {
        using (ApplicationContext db = new ApplicationContext())
        {
            db.Database.EnsureCreated();
            //FillUpDb(db);

            var company = db.Companies.FirstOrDefault(e => e.Id == 1);
            //var emp = db.Employees.FirstOrDefault(e => e.Id == 4);
            //var project = db.Projects.FirstOrDefault(e => e.Id == 3);

            //emp.CompanyId = company.Id;
            //emp.Position = "Specialist";

            //db.projectsTables.Add(new ProjectsTable
            //{
            //    EmployeeId = emp.Id,
            //    ProjectId = project.Id,
            //});

            //db.SaveChanges();


            var result = db.Projects.Where(p => p.ProjectsTable.Any(e => e.Employee.CompanyId == company.Id)).ToList();
                
        }
    }

    public static void FillUpDb(ApplicationContext db)
    {
            db.Companies.AddRange(
                new Company
                {
                    Name = "Google",
                },
                new Company 
                {
                    Name = "Apple",
                },
                new Company
                {
                    Name = "Microsoft"
                },
                new Company
                {
                    Name = "SpaceX"
                });

            db.Projects.AddRange(
                new Project
                {
                    Name = "New app",
                    Deadline = new DateTime(2024, 10, 10),
                },
                new Project
                {
                    Name = "Rocket",
                    Deadline = new DateTime(2030, 9, 10)
                },
                new Project
                {
                    Name = "Phone",
                    Deadline = new DateTime(2025, 8, 9)
                },
                new Project
                {
                    Name = "Android 30.1",
                    Deadline = new DateTime(2025, 1, 1)
                }
                );

            db.Employees.AddRange(
                new Employee
                {
                    Name = "Alex",
                    Age = 22,
                },
                new Employee
                {
                    Name = "Bob",
                    Age = 21
                },
                new Employee
                {
                    Name = "Tom",
                    Age = 25
                },
                new Employee
                {
                    Name = "Charly",
                    Age = 30
                },
                new Employee
                {
                    Name = "Shon",
                    Age = 29
                }
                );
        db.SaveChanges();
    }
}
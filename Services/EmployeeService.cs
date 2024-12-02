using hw7_1.Models;

namespace hw7_1.Services
{
    public class EmployeeService
    {
        public List<Employee> Employees { get; set; }
        public EmployeeService() 
        {
            Employees = new List<Employee>() 
            {
                new Employee { Name = "Alex", Salary = 100},
                new Employee { Name = "Bob", Salary = 100},
                new Employee { Name = "Tom", Salary = 100},
            };
        }
        public void AddEmployee(Employee employee) {Employees.Add(employee);}
        public Employee GetEmployeeById(int id) { return Employees[id];}
    }
}

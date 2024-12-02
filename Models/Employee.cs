using System.ComponentModel.DataAnnotations;

namespace hw7_1.Models
{
    public class Employee
    {
        static int id = 0;
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public decimal? Salary { get; set; }

        public Employee() 
        {
            Id = id++;
        }

    }
}

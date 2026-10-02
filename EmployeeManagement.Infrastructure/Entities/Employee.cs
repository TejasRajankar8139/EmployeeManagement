using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Infrastructure.Entities
{
    public class Employee
    {
        // Id(Guid or int)
        //FirstName(string, required)
        //LastName(string, required)
        //Email(string, required, unique)
        //Department(string)
        //DateOfJoining(DateTime)
        //IsActive(bool)

        public Guid Id { get; set; }
        [Required]
        public string? FirstName { get; set; }
        [Required]
        public string? LastName { get; set; }
        [Required]
        public string? Email { get; set; }
        public string? Department { get; set; }
        public DateTime DateOfJoining { get; set; }

        public bool IsActive;
    }
}

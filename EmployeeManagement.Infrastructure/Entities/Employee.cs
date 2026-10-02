using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Infrastructure.Entities
{
    public class Employee
    {
        public Guid Id { get; set; }

        [Required]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        public string LastName { get; set; } = string.Empty;

        [Required]
        public string Email { get; set; } = string.Empty;

        public string? Department { get; set; }

        public DateTime DateOfJoining { get; set; }

        public bool IsActive { get; set; }
    }
}

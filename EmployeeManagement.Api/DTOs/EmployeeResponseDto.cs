namespace EmployeeManagement.Api.DTOs
{
    public class EmployeeResponseDto
    {

        public Guid Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? Department { get; set; }
        public DateTime DateOfJoining { get; set; }
        public bool IsActive { get; set; }

    }
}

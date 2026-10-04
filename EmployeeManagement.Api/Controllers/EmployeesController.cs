using EmployeeManagement.Api.DTOs;
using EmployeeManagement.Infrastructure;
using EmployeeManagement.Infrastructure.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Api.Controllers
{
    [ApiController]
    [Route("Api/[Controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly AppDbContext _context;
        public EmployeesController(AppDbContext Context)
        {
            _context = Context;
        }

        //GET /api/employees(Returns a list of active employees, use LINQ .Where(e => e.IsActive))
        //GET /api/employees/{id} (Returns a single employee, returns 404 NotFound if missing)
        //POST /api/employees(Creates a new employee, returns 201 Created with the location header)
        //PUT /api/employees/{id} (Updates an existing employee, returns 404 if missing)
        //DELETE /api/employees/{id} (Soft delete: set IsActive = false instead of hard deleting. Return 204 NoContent).

        // GET: /api/employees
        // Returns a list of active employees
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployees()
        {
            var employees = await _context.Employees.AsNoTracking().Where(e => e.IsActive).Select(e => new EmployeeResponseDto
            {
                Id = e.Id,
                FirstName = e.FirstName,
                LastName = e.LastName,
                Email = e.Email,
                Department = e.Department,
                DateOfJoining = e.DateOfJoining,
                IsActive = e.IsActive,
            }).ToListAsync();

            return Ok(employees);
        }

        ////GET /api/employees/{id} (Returns a single employee, returns 404 NotFound if missing)
        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeResponseDto>> GetEmployeeById(Guid id)
        {
            var employee = await _context.Employees
                .Where(e => e.Id == id)
                .Select(e => new EmployeeResponseDto
                {
                    Id = e.Id,
                    FirstName = e.FirstName,
                    LastName = e.LastName,
                    Email = e.Email,
                    Department = e.Department,
                    DateOfJoining = e.DateOfJoining,
                    IsActive = e.IsActive
                })
                .SingleOrDefaultAsync();

            if (employee == null)
            {
                return NotFound();
            }

            return Ok(employee);
        }

        // POST /api/employees
        // Creates a new employee and returns 201 Created
        [HttpPost]
        //public async Task<ActionResult<Employee>> CreateEmployee(CreateEmployeeDto dto)
        //{
        //    var employee = new Employee
        //    {
        //        Id = Guid.NewGuid(),
        //        FirstName = dto.FirstName,
        //        LastName = dto.LastName,
        //        Email = dto.Email,
        //        Department = dto.Department,
        //        DateOfJoining = dto.DateOfJoining,
        //        IsActive = dto.IsActive
        //    };

        //    _context.Employees.Add(employee);

        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction(nameof(GetEmployeeById), new { id = employee.Id },
        //        employee);
        //}
        [HttpPost]
        public async Task<ActionResult<Employee>> CreateEmployee(CreateEmployeeDto dto)
        {
            var employee = new Employee
            {
                Id = Guid.NewGuid(),       // ⭐ Important
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                Department = dto.Department,
                DateOfJoining = dto.DateOfJoining,
                IsActive = true
            };

            _context.Employees.Add(employee);

            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetEmployeeById),
                new { id = employee.Id },
                employee);
        }

        //PUT /api/employees/{id} (Updates an existing employee, returns 404 if missing)

        [HttpPut("{id}")]
        public async Task<ActionResult<Employee>> UpdateEmployee(Guid id, UpdateEmployeeDto updateEmployee)
        {
            var existingEmployee = await _context.Employees.SingleOrDefaultAsync(e => e.Id == id);

            if (existingEmployee == null)
            {
                return NotFound();
            }

            existingEmployee.FirstName = updateEmployee.FirstName;
            existingEmployee.LastName = updateEmployee.LastName;
            existingEmployee.Email = updateEmployee.Email;
            existingEmployee.Department = updateEmployee.Department;
            existingEmployee.DateOfJoining = updateEmployee.DateOfJoining;
            existingEmployee.IsActive = updateEmployee.IsActive;

            await _context.SaveChangesAsync();

            return Ok(existingEmployee);
        }


        // DELETE /api/employees/{id}
        // Soft delete: sets IsActive = false instead of permanently deleting
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(Guid id)
        {
            var employee = await _context.Employees.SingleOrDefaultAsync(e => e.Id == id);
            if (employee == null)
            {
                return NotFound();
            }
            employee.IsActive = false;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpGet("test-error")]
        public IActionResult TestError()
        {
            throw new Exception("This is a test exception.");
        }
    }
}

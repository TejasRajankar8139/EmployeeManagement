using EmployeeManagement.Api.Controllers;
using EmployeeManagement.Api.DTOs;
using EmployeeManagement.Infrastructure;
using EmployeeManagement.Infrastructure.Entities;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace EmployeeManagement.Tests
{
    public class EmployeesControllerTests
    {
        private readonly DbContextOptions<AppDbContext> _options;

        public EmployeesControllerTests()
        {
            _options = new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
        }

        [Fact]
        public async Task GetEmployees_ReturnsOnlyActiveEmployees()
        {
            // Arrange
            await using var context = new AppDbContext(_options);

            var activeEmployee = new Employee
            {
                Id = Guid.NewGuid(),
                FirstName = "John",
                LastName = "Active",
                Email = "john@test.com",
                Department = "IT",
                DateOfJoining = DateTime.Now,
                IsActive = true
            };

            var inactiveEmployee = new Employee
            {
                Id = Guid.NewGuid(),
                FirstName = "Jane",
                LastName = "Inactive",
                Email = "jane@test.com",
                Department = "HR",
                DateOfJoining = DateTime.Now,
                IsActive = false
            };

            context.Employees.AddRange(activeEmployee, inactiveEmployee);
            await context.SaveChangesAsync();

            // Act
            var controller = new EmployeesController(context);
            var result = await controller.GetEmployees();

            // Assert
            // Assert
            result.Result.Should().BeOfType<OkObjectResult>();
            var okResult = result.Result as OkObjectResult;
            okResult.Should().NotBeNull();
            var employees = okResult!.Value as List<EmployeeResponseDto>;
            employees.Should().NotBeNull();
            employees!.Should().HaveCount(1);
            employees.First().Email.Should().Be("john@test.com");
        }

        [Fact]
        public async Task CreateEmployee_ValidDto_ReturnsCreatedAtAction()
        {
            // Arrange
            await using var context = new AppDbContext(_options);

            var controller = new EmployeesController(context);

            var dto = new CreateEmployeeDto
            {
                FirstName = "Tejas",
                LastName = "Rajankar",
                Email = "tejas@test.com",
                Department = "IT",
                DateOfJoining = DateTime.Now
            };

            // Act
            var result = await controller.CreateEmployee(dto);
        }

        [Fact]
        public async Task DeleteEmployee_ExistingId_SetsIsActiveToFalse()
        {
            // Arrange
            await using var context = new AppDbContext(_options);

            var employee = new Employee
            {
                Id = Guid.NewGuid(),
                FirstName = "Tejas",
                LastName = "Rajankar",
                Email = "tejas@test.com",
                Department = "IT",
                DateOfJoining = DateTime.Now,
                IsActive = true
            };

            context.Employees.Add(employee);

            await context.SaveChangesAsync();

            var controller = new EmployeesController(context);

            // Act
            var result = await controller.DeleteEmployee(employee.Id);
        }
    }
}
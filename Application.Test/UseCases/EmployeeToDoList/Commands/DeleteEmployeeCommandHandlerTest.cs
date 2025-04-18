using Application.Common.Interfaces;
using Application.Test.HelperServices;
using Application.UseCases.EmployeeToDoList.Commands;
using Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Test.UseCases.EmployeeToDoList.Commands
{
    [TestClass]
    public class DeleteEmployeeCommandHandlerTest
    {
        private Mock<IAppDbContext> _mockDbContext = null!;
        private DeleteEmployeeCommandHandler _handler = null!;
        private List<Employee> _employees = null!;

        [TestInitialize]
        public void Setup()
        {
            _mockDbContext = new Mock<IAppDbContext>();

            var employeeId = Guid.NewGuid();
            _employees = new List<Employee>
            {
                new Employee {Forenames = "Test", Surname = "User", Payroll_Number = "EMP001" }
            };

            // Setup DbSet mock
            var mockEmployeeDbSet = _employees.AsQueryable().BuildMockDbSet();
            _mockDbContext.Setup(x => x.Employees).Returns(mockEmployeeDbSet.Object);

            _mockDbContext.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()))
                          .ReturnsAsync(1);

            _handler = new DeleteEmployeeCommandHandler(_mockDbContext.Object);
        }

        [TestMethod]
        public async Task Handle_ValidId_ShouldDeleteEmployeeAndReturnTrue()
        {
            // Arrange
            var existingEmployeeId = _employees.First().Id;
            var command = new DeleteEmployeeCommand { Id = existingEmployeeId };

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.IsTrue(result);
            _mockDbContext.Verify(x => x.Employees.Remove(It.IsAny<Employee>()), Times.Once);
            _mockDbContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "User not found with Identifier")]
        public async Task Handle_InvalidId_ShouldThrowException()
        {
            // Arrange
            var command = new DeleteEmployeeCommand { Id = Guid.NewGuid() }; // non-existent ID

            // Act
            await _handler.Handle(command, CancellationToken.None);
        }
    }
}

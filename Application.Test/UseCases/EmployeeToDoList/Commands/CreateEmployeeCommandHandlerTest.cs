using Application.Common.Interfaces;
using Application.Models.EmployeeModels;
using Application.UseCases.EmployeeToDoList.Commands;
using AutoMapper;
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
    public class CreateEmployeeCommandHandlerTest
    {
        private Mock<IAppDbContext> _mockDbContext = null!;
        private IMapper _mapper = null!;
        private CreateEmployeeCommandHandler _handler = null!;

        [TestInitialize]
        public void Setup()
        {
            _mockDbContext = new Mock<IAppDbContext>();

            var mockEntityEntry = new Mock<Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<Employee>>();

            _mockDbContext.Setup(x => x.Employees.AddAsync(It.IsAny<Employee>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(mockEntityEntry.Object);

            _mockDbContext.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(1);

            // Setup AutoMapper
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CreateEmployeeCommand, Employee>();
                cfg.CreateMap<Employee, EmployeeViewModel>();
            });

            _mapper = config.CreateMapper();
            _handler = new CreateEmployeeCommandHandler(_mockDbContext.Object, _mapper);
        }


        [TestMethod]
        public async Task Handle_ValidRequest_ReturnsEmployeeViewModel()
        {
            // Arrange
            var command = new CreateEmployeeCommand
            {
                Payroll_Number = "1234",
                Forenames = "John",
                Surname = "Doe",
                Date_of_Birth = new DateOnly(1990, 1, 1),
                Telephone = "123456",
                Mobile = "987654",
                Address = "Main St",
                Address_2 = "Apt 1",
                Postcode = "10001",
                EMail_Home = "john.doe@example.com",
                Start_Date = new DateOnly(2023, 1, 1)
            };

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(command.Forenames, result.Forenames);
            Assert.AreEqual(command.Surname, result.Surname);
        }
    }
}

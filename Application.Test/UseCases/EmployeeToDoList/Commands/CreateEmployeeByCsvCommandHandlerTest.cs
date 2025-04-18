using Application.Common.Interfaces;
using Application.Models.CsvModels;
using Application.Models.EmployeeModels;
using Application.Services;
using Application.UseCases.EmployeeToDoList.Commands;
using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
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
    public class CreateEmployeeByCsvCommandHandlerTest
    {
        private Mock<IAppDbContext> _mockDbContext = null!;
        private Mock<IFileService> _mockFileService = null!;
        private IMapper _mapper = null!;
        private CreateEmployeeByCsvCommandHandler _handler = null!;

        [TestInitialize]
        public void Setup()
        {
            _mockDbContext = new Mock<IAppDbContext>();
            _mockFileService = new Mock<IFileService>();

            // AutoMapper setup
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Employee, EmployeeViewModel>();
            });
            _mapper = config.CreateMapper();

            _handler = new CreateEmployeeByCsvCommandHandler(_mockDbContext.Object, _mapper, _mockFileService.Object);
        }

        [TestMethod]
        public async Task Handle_ValidCsvFile_ReturnsMappedEmployeeList()
        {
            // Arrange
            var csvContent = @"Personnel_Records.Payroll_Number,Personnel_Records.Forenames,Personnel_Records.Surname,Personnel_Records.Date_of_Birth,Personnel_Records.Telephone,Personnel_Records.Mobile,Personnel_Records.Address,Personnel_Records.Address_2,Personnel_Records.Postcode,Personnel_Records.EMail_Home,Personnel_Records.Start_Date
                            COOP08,John,William,26/01/1955,12345678,987654231,12 Foreman road,London,GU12 6JW,nomadic20@hotmail.co.uk,18/04/2013
                            JACK13,Jerry,Jackson,11/5/1974,2050508,6987457,115 Spinney Road,Luton,LU33DF,gerry.jackson@bt.com,18/04/2013";

            var fileName = "employees.csv";
            var stream = new MemoryStream(Encoding.UTF8.GetBytes(csvContent));
            var formFile = new FormFile(stream, 0, stream.Length, "CsvFile", fileName)
            {
                Headers = new HeaderDictionary(),
                ContentType = "text/csv"
            };

            var command = new CreateEmployeeByCsvCommand
            {
                CsvFile = formFile
            };

            var mockEmployeeList = new List<Employee>
            {
                new() { Payroll_Number = "1234", Forenames = "John", Surname = "Doe" }
            };

            _mockFileService.Setup(x =>
                x.ReadCsvFileAsync<Employee, EmployeeCsvModel, CsvEmployeeMap>(It.IsAny<IFormFile>()))
                .ReturnsAsync(mockEmployeeList);

            _mockDbContext.Setup(x => x.Employees.AddRangeAsync(It.IsAny<IEnumerable<Employee>>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            _mockDbContext.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(1);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("John", result[0].Forenames);
        }
    }
}

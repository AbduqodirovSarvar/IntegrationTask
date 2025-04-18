using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetArchTest.Rules;
using Xunit;


namespace Architecture.Test
{
    public class CleanArchitectureTests
    {
        private const string DomainNamespace = "Domain";
        private const string ApplicationNamespace = "Application";
        private const string InfrastructureNamespace = "Infrastructure";
        private const string WebNamespace = "Web";

        [Fact]
        public void Domain_Should_Not_Have_Dependencies_On_Other_Projects()
        {
            var result = Types
                .InAssembly(typeof(Domain.Entities.Employee).Assembly)
                .ShouldNot()
                .HaveDependencyOnAny(ApplicationNamespace, InfrastructureNamespace, WebNamespace)
                .GetResult();

            Assert.IsTrue(result.IsSuccessful);
        }

        [Fact]
        public void Application_Should_Not_Depend_On_Web_Or_Infrastructure()
        {
            var result = Types
                .InAssembly(typeof(Application.UseCases.EmployeeToDoList.Commands.CreateEmployeeByCsvCommand).Assembly)
                .ShouldNot()
                .HaveDependencyOnAny(WebNamespace, InfrastructureNamespace)
                .GetResult();

            Assert.IsTrue(result.IsSuccessful);
        }

        [Fact]
        public void Infrastructure_Can_Depend_On_Application_And_Domain()
        {
            var result = Types
                .InAssembly(typeof(Infrastructure.Contexts.AppDbContext).Assembly)
                .ShouldNot()
                .HaveDependencyOn(WebNamespace)
                .GetResult();

            Assert.IsTrue(result.IsSuccessful);
        }
    }
}

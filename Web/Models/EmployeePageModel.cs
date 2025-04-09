using Application.Models.EmployeeModels;
using Application.UseCases.EmployeeToDoList.Commands;
using Application.UseCases.EmployeeToDoList.Queries;

namespace Web.Models
{
    public class EmployeePageModel
    {
        public List<EmployeeViewModel> Employees { get; set; } = [];
        public EmployeeViewModel Employee { get; set; } = new EmployeeViewModel();
        public CreateEmployeeCommand CreateEmployeeCommand { get; set; } = new CreateEmployeeCommand();
        public CreateEmployeeByCsvCommand CreateEmployeeByCsvCommand { get; set; } = new CreateEmployeeByCsvCommand();
        public DeleteEmployeeCommand DeleteEmployeeCommand { get; set; } = new DeleteEmployeeCommand();
        public UpdateEmployeeCommand UpdateEmployeeCommand { get; set; } = new UpdateEmployeeCommand();
        public GetEmployeeQuery GetEmployeeQuery { get; set; } = new GetEmployeeQuery();
        public GetAllEmployeeQuery GetAllEmployeeQuery { get; set; } = new GetAllEmployeeQuery();
    }
}

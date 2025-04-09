using System.Diagnostics;
using System.Threading.Tasks;
using Application.Models.EmployeeModels;
using Application.UseCases.EmployeeToDoList.Commands;
using Application.UseCases.EmployeeToDoList.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Web.Models;

namespace Web.Controllers;

public class HomeController(
    IMediator mediator) : Controller
{
    private readonly IMediator _mediator = mediator;
    private EmployeePageModel _employeePageModel = new();

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var employeeModels = await _mediator.Send(new GetAllEmployeeQuery());

        _employeePageModel.Employees = employeeModels;

        return View(_employeePageModel);
    }

    [HttpPost("employee/create")]
    public async Task<IActionResult> CreateEmployee(CreateEmployeeCommand command)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _mediator.Send(command);
        return PartialView("_NewEmployee", result);
    }

    [HttpPost("employee/create/csv")]
    public async Task<IActionResult> CreateEmployeeByCsv(CreateEmployeeByCsvCommand command)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _mediator.Send(command);
        return PartialView("_NewEmployee", result);
    }

    [HttpPost("employee/update")]
    public async Task<IActionResult> UpdateEmployee(UpdateEmployeeCommand command)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _mediator.Send(command);
        return PartialView("_UpdatedEmployee", result);
    }

    [HttpPost("employee/delete/{id}")]
    public async Task<IActionResult> DeleteEmployee(Guid id)
    {
        var result = await _mediator.Send(new DeleteEmployeeCommand { Id = id });
        return PartialView("_DeleteEmployee", result);
    }

    [HttpGet("employee/{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _mediator.Send(new GetEmployeeQuery { Id = id });
        return PartialView("_Employee", result);
    }

    [HttpGet("employee-list")]
    public async Task<IActionResult> GetEmployees([FromQuery] GetAllEmployeeQuery query)
    {
        var result = await _mediator.Send(query);

        var totalPages = (int)Math.Ceiling((double)result.Count / query.PaginationParams.PageSize);

        ViewData["TotalPages"] = totalPages;
        return PartialView("_EmployeeList", result);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

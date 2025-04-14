using System.Diagnostics;
using Application.UseCases.EmployeeToDoList.Commands;
using Application.UseCases.EmployeeToDoList.Queries;
using AutoMapper;
using Domain.Configurations;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Web.Models;
using Web.Services;

namespace Web.Controllers;

public class HomeController(
    IMediator mediator,
    IMapper mapper,
    SingleModalHelper singleModalHelper
    ) : Controller
{
    private readonly IMediator _mediator = mediator;
    private readonly IMapper _mapper = mapper;
    private EmployeePageModel _employeePageModel = singleModalHelper.Model;

    [HttpGet]
    public async Task<IActionResult> Index(int pageIndex = 0, int pageSize = 10)
    {
        ViewData["CurrentPage"] = pageIndex + 1;
        ViewData["PageSize"] = pageSize;
        var result = await _mediator.Send(new GetAllEmployeeQuery()
                                                {
            PaginationParams = new PaginationParams
            {
                PageIndex = pageIndex,
                PageSize = pageSize
            },
            Filter = new Filter
            {
                SearchingText = null,
                Ascending = false
            }
        });

        if (result?.Data != null)
        {
            _employeePageModel.Employees = result.Data;
            ViewData["TotalPages"] = result.TotalCount;
            ViewData["CurrentPage"] = result.PageIndex;
            ViewData["PageSize"] = result.PageSize;
        }

        return View(_employeePageModel);
    }

    public async Task<IActionResult> CreateEmployee(CreateEmployeeCommand command)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        await _mediator.Send(command);
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> CreateEmployeeByCsv(CreateEmployeeByCsvCommand command)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        await _mediator.Send(command);
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> UpdateEmployee(Guid id)
    {
        var result = await _mediator.Send(new GetEmployeeQuery { Id = id });

        if (result == null)
            return NotFound();

        var updateModel = _mapper.Map<UpdateEmployeeCommand>(result);
        _employeePageModel.UpdateEmployeeCommand = updateModel;

        return View(updateModel);
    }

    public async Task<IActionResult> ClickUpdateEmployee(UpdateEmployeeCommand command)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _mediator.Send(command);
        _employeePageModel.UpdateEmployeeCommand = new UpdateEmployeeCommand();
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> DeleteEmployee(Guid id)
    {
        await _mediator.Send(new DeleteEmployeeCommand { Id = id });
        return RedirectToAction("Index");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

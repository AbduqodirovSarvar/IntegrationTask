using Application.UseCases.EmployeeToDoList.Commands;
using Application.UseCases.EmployeeToDoList.Queries;
using AutoMapper;
using Domain.Configurations;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
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
    private readonly EmployeePageModel _employeePageModel = singleModalHelper.Model;

    [HttpGet]
    public async Task<IActionResult> Index(int pageIndex = 0, int pageSize = 10, string? searchingText = null, bool ascending = false)
    {
        ViewData["PageIndex"] = pageIndex;
        ViewData["PageSize"] = pageSize;
        ViewData["SearchingText"] = searchingText;
        ViewData["Ascending"] = ascending;
        var result = await _mediator.Send(new GetAllEmployeeQuery()
        {
            PaginationParams = new PaginationParams
            {
                PageIndex = pageIndex,
                PageSize = pageSize
            },
            Filter = new Filter
            {
                SearchingText = searchingText,
                Ascending = ascending
            }
        });

        if (result?.Data != null)
        {
            _employeePageModel.Employees = result.Data;
            ViewData["Total"] = result.TotalCount;
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

    public async Task<IActionResult> OpenUpdateEmployeePage(Guid id)
    {
        var result = await _mediator.Send(new GetEmployeeQuery { Id = id });

        if (result == null)
            return NotFound();

        var updateModel = _mapper.Map<UpdateEmployeeCommand>(result);
        return View("UpdateEmployeePage", updateModel);
    }

    public async Task<IActionResult> ClickUpdateEmployee(UpdateEmployeeCommand command)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        await _mediator.Send(command);
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

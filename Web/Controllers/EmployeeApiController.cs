using Application.UseCases.EmployeeToDoList.Commands;
using Application.UseCases.EmployeeToDoList.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeApiController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpPost("employee/create")]
        public async Task<IActionResult> CreateEmployee([FromBody] CreateEmployeeCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost("employee/create/csv")]
        public async Task<IActionResult> CreateEmployeeByCsv([FromForm] CreateEmployeeByCsvCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost("employee/update")]
        public async Task<IActionResult> UpdateEmployee([FromBody] UpdateEmployeeCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost("employee/delete/{id}")]
        public async Task<IActionResult> DeleteEmployee(Guid id)
        {
            var result = await _mediator.Send(new DeleteEmployeeCommand { Id = id });
            return Ok(result);
        }

        [HttpGet("employee/{id}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var result = await _mediator.Send(new GetEmployeeQuery { Id = id });
            return Ok(result);
        }

        [HttpGet("employee-list")]
        public async Task<IActionResult> GetEmployees([FromQuery] GetAllEmployeeQuery query)
        {
            var result = await _mediator.Send(query);

            return Ok(result);
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using MediatR;
using CleanArchitectureDemo.Application.Expenses.Commands;

namespace ExpenseTracker.API.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class ExpensesController : ControllerBase
{
    private readonly IMediator _mediator;

    public ExpensesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateExpenseCommand command)
    {
        var id = await _mediator.Send(command);
        return Ok(id);
    }
[HttpPut]
    public async Task<IActionResult> Create(UpdateCommand command)
    {
        var id = await _mediator.Send(command);
        return Ok(id);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
         var expenses = await _mediator.Send(new GetAllExpensesQuery());
         return Ok(expenses);
        //return Ok("Get All Expenses");
    }
    [HttpGet]
    public async Task<IActionResult> GetVerify()
    {
         var expenses = await _mediator.Send(new GetAllExpensesQuery());
         //return Ok(expenses);
        return Ok("Get All Expenses");
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _mediator.Send(new DeleteCommand { ExpenseId = id });
        if (result == 0)
        {
            return NotFound();
        }
        return NoContent();
    }

    // Similarly Add Get, Update, Delete
}

using BankProject.Application.CQRS.Commands;
using BankProject.Application.CQRS.Queries;
using BankProject.Application.DTOs;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace BankProject.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class TransactionController : ControllerBase
{
    private readonly IMediator _mediator;

    public TransactionController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpPost]
    public async Task<IActionResult> InternalTransfer(CreateTransactionCommand command)
    {
        var response = await _mediator.Send(command);

        return Ok(response);
    }
    
    [HttpGet]
    [Route("{accountId}")]
    public async Task<IActionResult> GetAllTransactions(int accountId)
    {
        var query = new GetAllTransactionsByAccountIdQuery();
        query.AccountId = accountId;

        var response = await _mediator.Send(query);

        return Ok(response);
    }
}
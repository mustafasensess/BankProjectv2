using BankProject.Application.CQRS.Queries;
using BankProject.Persistence.Context;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BankProject.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class AccountsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ApplicationDbContext _context;

    public AccountsController(IMediator mediator, ApplicationDbContext context)
    {
        _mediator = mediator;
        _context = context;
    }
    
    [HttpGet]
    [Route("{userId}")]
    public async Task<IActionResult> GetAll(string userId)
    {
        var query = new GetAllAccountsQuery();
        query.UserId = userId;
        var response = await _mediator.Send(query);

        return Ok(response);
    }
    
    [HttpGet]
    [Route("{id}/balance")]
    //[Authorize]
    public async Task<IActionResult> GetBalanceById(int id)
    {
        var query = new GetBalanceByIdQuery();
        query.Id = id;
        var response = await _mediator.Send(query);

        return Ok(response);
    }
}
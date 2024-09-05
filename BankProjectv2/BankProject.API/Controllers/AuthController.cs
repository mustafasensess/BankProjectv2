using BankProject.Application.CQRS.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BankProject.API.Controllers;
[ApiController]
[Route("api/v1/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login(LoginUserCommand command)
    {
        var response = await _mediator.Send(command);

        return Ok(response);
    }
    
    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> Register(CreateUserCommand command)
    {
        var response = await _mediator.Send(command);

        return Ok(response);
    }
}
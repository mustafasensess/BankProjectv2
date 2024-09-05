using BankProject.Application.DTOs;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace BankProject.Application.CQRS.Commands;

public class CreateUserCommand : IRequest<IdentityResult>
{
    public CreateUserRequestDto CreateUserRequestDto { get; set; }
}
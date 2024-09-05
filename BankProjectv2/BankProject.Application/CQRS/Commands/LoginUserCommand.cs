using BankProject.Application.DTOs;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace BankProject.Application.CQRS.Commands;

public class LoginUserCommand : IRequest<string>
{
    public LoginRequestDto LoginRequestDto { get; set; }
}
using BankProject.Application.DTOs;
using MediatR;

namespace BankProject.Application.CQRS.Queries;

public class GetAllAccountsQuery : IRequest<List<AccountDto>>
{
    public string UserId { get; set; }
}
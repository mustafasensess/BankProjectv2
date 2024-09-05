using BankProject.Domain.Entities;
using MediatR;

namespace BankProject.Application.CQRS.Queries;

public class GetAllTransactionsByAccountIdQuery : IRequest<List<Transaction>>
{
    public int AccountId { get; set; }
}
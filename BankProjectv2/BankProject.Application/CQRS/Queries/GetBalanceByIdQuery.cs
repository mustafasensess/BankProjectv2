using MediatR;

namespace BankProject.Application.CQRS.Queries;

public class GetBalanceByIdQuery : IRequest<decimal>
{
    public int Id { get; set; }
}
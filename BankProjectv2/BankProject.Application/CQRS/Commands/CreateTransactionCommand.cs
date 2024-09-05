using BankProject.Application.DTOs;
using MediatR;

namespace BankProject.Application.CQRS.Commands;

public class CreateTransactionCommand : IRequest<CreateTransactionDto>
{
    public int SenderId { get; set; }

    public int ReceiverId { get; set; }

    public decimal Amount { get; set; }
}
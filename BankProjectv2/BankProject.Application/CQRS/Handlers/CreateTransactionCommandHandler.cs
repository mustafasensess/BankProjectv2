using BankProject.Application.CQRS.Commands;
using BankProject.Application.DTOs;
using BankProject.Domain.Entities;
using BankProject.Persistence.Context;
using MediatR;

namespace BankProject.Application.CQRS.Handlers;

public class CreateTransactionCommandHandler : IRequestHandler<CreateTransactionCommand, CreateTransactionDto>
{
    private readonly ApplicationDbContext _dbContext;

    public CreateTransactionCommandHandler(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<CreateTransactionDto> Handle(CreateTransactionCommand request, CancellationToken cancellationToken)
    {
        if (request.Amount <= 0)
        {
            throw new NullReferenceException();
        }

        if(request.ReceiverId == request.SenderId)
        {
            throw new NullReferenceException();
        }

        var sender = await _dbContext.Accounts.FindAsync(request.SenderId);
        if (sender == null)
        {
            throw new NullReferenceException();
        }

        if (sender.Balance < request.Amount)
        {
            throw new NullReferenceException();
        }

        var receiver = await _dbContext.Accounts.FindAsync(request.ReceiverId);
        if (receiver == null)
        {
            throw new NullReferenceException();
        }

        receiver.Balance += request.Amount;
        sender.Balance -= request.Amount;
        
        var transaction = new Transaction
        {
            Amount = request.Amount,
            ReceiverId = request.ReceiverId,
            SenderId = request.SenderId
        };
        await _dbContext.Transactions.AddAsync(transaction, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        var createTransactionDto = new CreateTransactionDto();
        
        createTransactionDto.Amount = request.Amount;
        createTransactionDto.ReceiverId = request.ReceiverId;
        createTransactionDto.SenderId = request.SenderId;
        
        return createTransactionDto;
    }
}
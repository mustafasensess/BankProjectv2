using BankProject.Application.CQRS.Queries;
using BankProject.Domain.Entities;
using BankProject.Persistence.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BankProject.Application.CQRS.Handlers;

public class GetAllTransactionsByAccountIdQueryHandler : IRequestHandler<GetAllTransactionsByAccountIdQuery, List<Transaction>>
{
    private readonly ApplicationDbContext _dbContext;

    public GetAllTransactionsByAccountIdQueryHandler(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<List<Transaction>> Handle(GetAllTransactionsByAccountIdQuery request, CancellationToken cancellationToken)
    {
        var account = await _dbContext.Accounts.FindAsync(request.AccountId);
        if (account == null)
        {
            throw new NullReferenceException();
        }

        var transactions = 
            await _dbContext.Transactions.Where(t => t.SenderId == request.AccountId || t.ReceiverId == request.AccountId)
                .AsNoTracking().ToListAsync();
        return transactions;
    }
}
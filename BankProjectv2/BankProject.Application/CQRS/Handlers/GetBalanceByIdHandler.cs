using BankProject.Application.CQRS.Queries;
using BankProject.Persistence.Context;
using MediatR;

namespace BankProject.Application.CQRS.Handlers;

public class GetBalanceByIdHandler : IRequestHandler<GetBalanceByIdQuery, decimal>
{
    private readonly ApplicationDbContext _context;

    public GetBalanceByIdHandler(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<decimal> Handle(GetBalanceByIdQuery request, CancellationToken cancellationToken)
    {
        var account = await _context.Accounts.FindAsync(request.Id);
        if (account == null)
        {
            throw new NullReferenceException();
        }

        return account.Balance;
    }
}
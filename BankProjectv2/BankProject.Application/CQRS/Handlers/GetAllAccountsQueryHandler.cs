using AutoMapper;
using BankProject.Application.CQRS.Queries;
using BankProject.Application.DTOs;
using BankProject.Domain.Entities;
using BankProject.Persistence.Context;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BankProject.Application.CQRS.Handlers;

public class GetAllAccountsQueryHandler : IRequestHandler<GetAllAccountsQuery, List<AccountDto>>
{
    private readonly UserManager<User> _userManager;
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetAllAccountsQueryHandler(UserManager<User> userManager, ApplicationDbContext context, IMapper mapper)
    {
        _userManager = userManager;
        _context = context;
        _mapper = mapper;
    }
    public async Task<List<AccountDto>> Handle(GetAllAccountsQuery request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByNameAsync(request.UserId);
        if (user == null)
        {
            throw new NullReferenceException("User not found!");
        }
        
        var accounts = _context.Accounts.Where(a => a.UserId == request.UserId).AsNoTracking().ToList();
        
        var accountsDtoList = _mapper.Map<List<AccountDto>>(accounts);

        return accountsDtoList;
    }
}
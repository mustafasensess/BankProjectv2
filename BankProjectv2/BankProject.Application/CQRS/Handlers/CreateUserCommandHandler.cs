using AutoMapper;
using BankProject.Application.CQRS.Commands;
using BankProject.Domain.Entities;
using BankProject.Persistence.Context;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace BankProject.Application.CQRS.Handlers;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand,IdentityResult>
{
    private readonly IMapper _mapper;
    private readonly UserManager<User> _userManager;
    private readonly ApplicationDbContext _dbContext;

    public CreateUserCommandHandler(IMapper mapper, UserManager<User> userManager, ApplicationDbContext dbContext)
    {
        _mapper = mapper;
        _userManager = userManager;
        _dbContext = dbContext;
    }
    public async Task<IdentityResult> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = _mapper.Map<User>(request);
        user.UserName = request.CreateUserRequestDto.AccountNo.ToString();
        var result = await _userManager.CreateAsync(user, request.CreateUserRequestDto.Password);
        if (!result.Succeeded)
            return result;
        var registeredUser = await _userManager.FindByNameAsync(user.UserName);
        var account = new Account
        {
            Balance = request.CreateUserRequestDto.Balance,
            UserId = registeredUser.Id
        };
        await _dbContext.Accounts.AddAsync(account);
        await _dbContext.SaveChangesAsync();
        return result;
    }
}
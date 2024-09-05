using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BankProject.Application.CQRS.Commands;
using BankProject.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace BankProject.Application.CQRS.Handlers;

public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand,string>
{
    private readonly UserManager<User> _userManager;
    private readonly IConfiguration _configuration;

    public LoginUserCommandHandler(UserManager<User> userManager, IConfiguration configuration)
    {
        _userManager = userManager;
        _configuration = configuration;
    }
    public async Task<string> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByNameAsync(request.LoginRequestDto.Username);

        if (user == null)
            throw new NullReferenceException();
        var checkPasswordResult = await _userManager.CheckPasswordAsync(user, request.LoginRequestDto.Password);

        if (!checkPasswordResult)
            throw new NullReferenceException();

        var jwtToken = CreateJwtToken(user);

        return jwtToken;
    }
    public string CreateJwtToken(IdentityUser user)
    {
        //Create claims
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.UserName)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(60),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
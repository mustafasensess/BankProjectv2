using Microsoft.AspNetCore.Identity;

namespace BankProject.Domain.Entities;

public class User : IdentityUser
{
    public int AccountNo { get; set; }
}
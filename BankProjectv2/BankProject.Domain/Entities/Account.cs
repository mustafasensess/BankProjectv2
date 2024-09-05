namespace BankProject.Domain.Entities;

public class Account
{
    public int Id { get; set; }
    
    public decimal Balance { get; set; }

    public string UserId { get; set; }
    
    public User User { get; set; }
}
﻿namespace BankProject.Application.DTOs;

public class CreateUserRequestDto
{
    public int AccountNo { get; set; }
    public string Password { get; set; } = string.Empty;

    public decimal Balance { get; set; }
}
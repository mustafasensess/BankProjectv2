﻿namespace BankProject.Application.DTOs;

public class LoginRequestDto
{
    public string Username { get; set; } = string.Empty;

    public string Password { get; set; }
}
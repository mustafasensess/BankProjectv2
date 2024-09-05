using AutoMapper;
using BankProject.Application.DTOs;
using BankProject.Domain.Entities;

namespace BankProject.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Account, AccountDto>().ReverseMap();
        CreateMap<CreateUserRequestDto, User>().ReverseMap();
    }
}
using AutoMapper;
using Questao5.Domain.Entities;
using Questao5.Infrastructure.Database.DTOs;

namespace Questao5.Domain.Profiles;

public class MovimentaContaProfile : Profile
{
    public MovimentaContaProfile()
    {
        CreateMap<MovimentaContaDTO, MovimentaContaEntity>();
    }
}

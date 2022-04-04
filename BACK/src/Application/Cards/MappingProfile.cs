using AutoMapper;
using Domain.Entities;

namespace Application.Cards
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Card, Dtos.CardDto>();
        }
    }
}

using AutoMapper;
using Rubicon.Wow.CleanCode.Example.Domain.DTO;

namespace Rubicon.Wow.CleanCode.Example.Domain.Mappings;

public class CharacterProfile : Profile
{
    public CharacterProfile()
    {
        CreateMap<DisneyCharacter, CharacterDTO>()
            .ForMember(x => x.Name, opt => opt.MapFrom(src => src.name))
            .ForMember(x => x.FilmsCount, opt => opt.MapFrom(src => src.films.Count))
            .ForMember(x => x.VideogamesCount, opt => opt.MapFrom(src => src.videoGames.Count));

        CreateMap<CharacterDTO, DisneyCharacter>();
    }
}

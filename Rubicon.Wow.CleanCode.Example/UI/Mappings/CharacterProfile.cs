using AutoMapper;

namespace Rubicon.Wow.CleanCode.Example.UI;

public class CharacterProfile : Profile
{
    public CharacterProfile()
    {
        CreateMap<DisneyCharacter, CharacterViewModel>()
            .ForMember(x => x.Name, opt => opt.MapFrom(src => src.name))
            .ForMember(x => x.FilmsCount, opt => opt.MapFrom(src => src.videoGames.Count))
            .ForMember(x => x.VideogamesCount, opt => opt.MapFrom(src => src.videoGames.Count));

        CreateMap<CharacterViewModel, DisneyCharacter>();
    }
}

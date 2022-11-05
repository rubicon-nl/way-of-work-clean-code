using Rubicon.Wow.CleanCode.Data;

namespace Rubicon.Wow.CleanCode.Example.Infrastructure;
public interface IDisneyCharacterRepository
{
    Task<DisneyCharacters> GetAsync();
}



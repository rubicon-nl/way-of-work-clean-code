namespace Rubicon.Wow.CleanCode.Example.Domain
{
    public interface IDisneyCharacterRepository
    {
        Task<List<DisneyCharacter>> GetDisneyCharacters();
    }
}
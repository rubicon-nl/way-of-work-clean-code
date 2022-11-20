namespace Rubicon.Wow.CleanCode.Example.Domain
{
    /// <summary>
    /// The Disney charactier repository
    /// </summary>
    public interface IDisneyCharacterRepository
    {
        /// <summary>
        /// Gets the all Disney characters.
        /// </summary>
        /// <returns></returns>
        Task<List<DisneyCharacter>> GetDisneyCharacters();
    }
}
namespace Rubicon.Wow.CleanCode.Example.Domain
{
    /// <summary>
    /// The Disney character service.
    /// </summary>
    public interface IDisneyCharacterService
    {
        /// <summary>
        /// Creates the super hero squad.
        /// </summary>
        /// <param name="amount"></param>
        /// <returns></returns>
        Task CreateSuperHeroSquad(int amount);

        /// <summary>
        /// Gets the top game appearances.
        /// </summary>
        /// <param name="amount"></param>
        /// <returns></returns>
        Task TopGameAppearances(int amount);

        /// <summary>
        /// Gets the top movie appearances.
        /// </summary>
        /// <param name="amount"></param>
        /// <returns></returns>
        Task TopMovieAppearances(int amount);

        /// <summary>
        /// Stores the given character.
        /// </summary>
        /// <param name="character"></param>
        /// <returns></returns>
        Task StoreCharacter(DisneyCharacter character);
    }
}
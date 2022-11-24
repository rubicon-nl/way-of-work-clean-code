using Rubicon.Wow.CleanCode.Example.Domain.DTO;

namespace Rubicon.Wow.CleanCode.Example.Domain
{
    /// <summary>
    /// Presents the given Disney characters.
    /// </summary>
    public interface ICharacterPresenter
    {
        /// <summary>
        /// Presents Top Movie Appearances.
        /// </summary>
        /// <param name="characters"></param>
        /// <returns></returns>
        public Task ShowTopMovieAppearances(IEnumerable<CharacterDTO> characters);

        /// <summary>
        /// Presents Top Game Appearances.
        /// </summary>
        /// <param name="characters"></param>
        /// <returns></returns>
        public Task ShowTopGameAppearances(IEnumerable<CharacterDTO> characters);

        /// <summary>
        /// Presents  the super hero squad.
        /// </summary>
        /// <param name="names"></param>
        /// <returns></returns>
        public Task ShowSuperHeroSquad(IEnumerable<string> names);
    }
}
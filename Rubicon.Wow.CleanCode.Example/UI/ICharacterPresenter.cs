namespace Rubicon.Wow.CleanCode.Example.UI
{
    public interface ICharacterPresenter
    {
        public Task ShowTopMovieAppearances(IEnumerable<CharacterViewModel> characters);

        public Task ShowTopGameAppearances(IEnumerable<CharacterViewModel> characters);

        public Task ShowSuperHeroSquad(IEnumerable<string> names);
    }
}
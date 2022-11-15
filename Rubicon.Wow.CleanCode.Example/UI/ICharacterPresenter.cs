namespace Rubicon.Wow.CleanCode.Example.UI
{
    public interface ICharacterPresenter
    {
        public Task ShowTopMovieAppearances(IEnumerable<CharacterViewModel> characters);
    }
}
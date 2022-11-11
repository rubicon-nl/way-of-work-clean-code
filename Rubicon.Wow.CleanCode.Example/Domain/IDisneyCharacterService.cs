namespace Rubicon.Wow.CleanCode.Example.Domain
{
    public interface IDisneyCharacterService
    {
        Task CreateSuperHeroSquad(int amount);
        Task TopGameAppearances(int amount);
        Task TopMovieAppearances(int amount);
    }
}
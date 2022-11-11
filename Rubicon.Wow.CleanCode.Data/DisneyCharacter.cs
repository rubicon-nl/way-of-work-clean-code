namespace Rubicon.Wow.CleanCode.Data;
public class DisneyCharacter
{
    public List<string> films { get; set; }
    public List<string> shortFilms { get; set; }
    public List<string> tvShows { get; set; }
    public List<string> videoGames { get; set; }
    public List<string> parkAttractions { get; set; }
    public List<string> allies { get; set; }
    public List<string> enemies { get; set; }
    public int _id { get; set; }
    public string name { get; set; }
    public string imageUrl { get; set; }
    public string url { get; set; }
}
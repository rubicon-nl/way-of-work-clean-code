using Rubicon.Wow.CleanCode.Example;

Console.WriteLine("Start fetching Disney characters");

var page = 1;
var totalPages = 1;

HttpClient client = new HttpClient();
var cumulatedCharacters = new List<DisneyCharacter>();

// Retrieve all disney characters
do
{
    Console.WriteLine($"Retrieving page {page}");
    var httpResponse = await client.GetAsync($"https://api.disneyapi.dev/characters?page={page}");

    if (httpResponse.IsSuccessStatusCode)
    {
        if (httpResponse.Content is object && httpResponse.Content.Headers.ContentType.MediaType == "application/json")
        {
            var contentStream = await httpResponse.Content.ReadAsStreamAsync();

            var characters = await JsonSerializer.DeserializeAsync<DisneyCharacters>(contentStream);
            try
            {
                cumulatedCharacters.AddRange(characters.data);
                totalPages = characters.totalPages;
            } catch (NullReferenceException)
            {
                Console.WriteLine("Characters is null");
            }
        }
        else
        {
            Console.WriteLine("HTTP Response was invalid and cannot be deserialised.");
        }
    } else
    {
        Console.WriteLine("HTTP Response error");
    }
    
    page++;
} while (page <= totalPages);



Console.WriteLine("Top 5 character movie appearances");

// find top 5 disney characters with most movie appearances
var t5cma = cumulatedCharacters.OrderByDescending(x => x.films.Count).Take(5);
int i = 1;

foreach (var item in t5cma)
{
    Console.WriteLine($"{i}. {item.name} ({item.films.Count})");
    i++;
}



Console.WriteLine("Top 5 character game appearances");

// find top 5 disney characters with most movie appearances
var t5cga = cumulatedCharacters.OrderByDescending(x => x.videoGames.Count).Take(5);
i = 1;

foreach (var item in t5cga)
{
    Console.WriteLine($"{i}. {item.name} ({item.videoGames.Count})");
    i++;
}



Console.WriteLine("Create superhero squad of most favored allies");

// create a superhero squad of most favored allies
var mostFavoredAllies = cumulatedCharacters
    .SelectMany(x => x.allies)
    .GroupBy(x => x)
    .Select(g => new { Name = g.Key, Count = g.Count() })
    .OrderByDescending(x => x.Count)
    .Select(x => x.Name)
    .Take(5);

if (mostFavoredAllies != null)
{
    foreach (var item in mostFavoredAllies)
    {
        Console.WriteLine($"{item}");
    }
}
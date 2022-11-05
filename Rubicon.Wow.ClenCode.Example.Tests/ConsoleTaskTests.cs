using Moq;
using Rubicon.Wow.CleanCode.Data;
using Rubicon.Wow.CleanCode.Example.Infrastructure;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Rubicon.Wow.CleanCode.Example.Tests;
public class ConsoleTaskTests
{
    private readonly TestOutputWriter _outputWriter;

    public ConsoleTaskTests(ITestOutputHelper output)
    {
        _outputWriter = new TestOutputWriter(output);
    }

    [Fact]
    public async Task ExecuteAsync_ShouldCallProcessingFunctions_AndReturnsTrue()
    {
        // Arrange
        Mock<IDisneyCharacterRepository> characterRepositoryMock = new Mock<IDisneyCharacterRepository>();
        
        var mickeyMouse = CharacterBuilder.Create(1, "Mickey Mouse")
           .PlayedInMovies("Mickey Mouse", "THE THREE MUSKETEERS", "Fanatasia 200", "World of Illusion").Build();
        var donaldDuck = CharacterBuilder.Create(1, "Donald Duck")
            .PlayedInMovies("Donald and Pluto ", "Self Control", "World of Illusion").Build();
        var goofy = CharacterBuilder.Create(1, "Goofy")
            .PlayedInMovies("World of Illusion").Build();

        var retrievedChars = new DisneyCharacter[] { mickeyMouse, goofy, donaldDuck };
        characterRepositoryMock.Setup(_ => _.GetAsync()).Returns(Task.FromResult(new DisneyCharacters { Data = retrievedChars }));
        
        ConsoleTask task = new(_outputWriter, characterRepositoryMock.Object);

        // Act
        var result = await task.ExecuteAsync();

        // Assert        
        Assert.True(result);
    }
}
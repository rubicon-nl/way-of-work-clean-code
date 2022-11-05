using FluentAssertions;
using Rubicon.Wow.CleanCode.Data;
using Rubicon.Wow.CleanCode.Example.Infrastructure;
using Rubicon.Wow.CleanCode.Example.Tests.TestData;
using Rubicon.Wow.CleanCode.Example.Tests.TestSupport;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Rubicon.Wow.CleanCode.Example.Tests;
public class DisneyCharacterRepositoryTests
{
    private readonly TestOutputWriter _outputWriter;

    public DisneyCharacterRepositoryTests(ITestOutputHelper output)
    {
        _outputWriter = new TestOutputWriter(output);
    }

    [Fact]
    public async Task FetchCharacters_ShouldRetrieveCharacters_AndReturnAListOfCharacters()
    {
        // Arrange
        var mickeyMouse = CharacterBuilder.Create(1, "Mickey Mouse")
            .PlayedInMovies("Mickey Mouse", "THE THREE MUSKETEERS", "Fanatasia 200", "World of Illusion").Build();
        var donaldDuck = CharacterBuilder.Create(1, "Donald Duck")
            .PlayedInMovies("Donald and Pluto ", "Self Control", "World of Illusion").Build();

        DisneyCharacters characters = new DisneyCharacters { Count = 2, TotalPages = 1, NextPage = null, Data = (new DisneyCharacter[] { mickeyMouse, donaldDuck }) };

        var httpDecorator = await HttpFactoryStub.GetHttpDecorator(characters);
        DisneyCharacterRepository disneyCharacterRepository = new DisneyCharacterRepository(httpDecorator, _outputWriter);

        // Act
        var resultOfService = await disneyCharacterRepository.GetAsync();

        // Assert
        resultOfService.Should().Equal(disneyCharacterRepository.DisneyCharacters);
    }
}

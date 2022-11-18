using FluentAssertions;
using Rubicon.Wow.CleanCode.Data;
using Rubicon.Wow.CleanCode.Example.Domain;
using Rubicon.Wow.CleanCode.Example.Tests.TestData;
using Rubicon.Wow.CleanCode.Example.Tests.TestSupport;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace Rubicon.Wow.CleanCode.Example.Tests;

public class DisneyCharacterServiceTests
{
    private readonly TestOutputWriter _outputWriter;

    public DisneyCharacterServiceTests(ITestOutputHelper output)
    {
        _outputWriter = new TestOutputWriter(output);
    }

    [Fact]
    public void GetTopDisneyCharactersWithMostMovieAppeance_OnlyTwoOfThree_ShouldReturnAsTopMost()
    {
        // Arrange        
        var mickeyMouse = CharacterBuilder.Create(1, "Mickey Mouse")
            .PlayedInMovies("Mickey Mouse", "THE THREE MUSKETEERS", "Fanatasia 200", "World of Illusion").Build();
        var donaldDuck = CharacterBuilder.Create(1, "Donald Duck")
            .PlayedInMovies("Donald and Pluto ", "Self Control", "World of Illusion").Build();
        var goofy = CharacterBuilder.Create(1, "Goofy")
            .PlayedInMovies("World of Illusion").Build();

        var retrievedChars = new DisneyCharacter[] { mickeyMouse, goofy, donaldDuck };
        DisneyCharacterService service = new DisneyCharacterService(retrievedChars.AsQueryable());

        // Act
        var top2 = service.GetTopDisneyCharactersWithMostMovieAppeances(2);

        // Assert
        top2.Should().Equal(new List<DisneyCharacter> { mickeyMouse, donaldDuck });
    }


    [Theory]
    [ClassData(typeof(CharacterGeneratoTheoryData))]
    public void GetTopDisneyCharacters_WithTwoMostMovieAppeances_ShouldReturnTopTwoCharactersWithMostMovieCount(params DisneyCharacter[] testData)
    {
        // arrange
        _outputWriter.WriteLine(string.Join(",", testData.Select(f => f.Name)));
        DisneyCharacterService service = new DisneyCharacterService(testData.AsQueryable());

        // Act
        var top2 = service.GetTopDisneyCharactersWithMostMovieAppeances(2);

        // Assert
        top2.Should().HaveSameCount(testData.Where(f => f.Name != "Goofy"));


    }

    [Theory]
    [InlineData(1, 1)]
    [InlineData(2, 2)]
    [InlineData(3, 3)]
    public void GetTopDisneyCharactersWithMostVideoGameAppeances_ShouldMatchRequestCount(int exampleCount, int requestCount)
    {
        // Arrange        
        var mickeyMouse = CharacterBuilder.Create(1, "Mickey Mouse")
            .PlayedInMovies("Mickey Mouse", "THE THREE MUSKETEERS", "Fanatasia 200", "World of Illusion").Build();
        var donaldDuck = CharacterBuilder.Create(1, "Donald Duck")
            .PlayedInMovies("Donald and Pluto ", "Self Control", "World of Illusion").Build();
        var goofy = CharacterBuilder.Create(1, "Goofy")
            .PlayedInMovies("World of Illusion").Build();

        var retrievedChars = new DisneyCharacter[] { mickeyMouse, goofy, donaldDuck }.Take(exampleCount);
        DisneyCharacterService service = new DisneyCharacterService(retrievedChars.AsQueryable());

        // Act
        var top2 = service.GetTopDisneyCharactersWithMostVideoGameAppeances(requestCount);

        // Assert
        top2.Should().HaveSameCount(retrievedChars);
    }


    [Fact]
    public void GetTopDisneyCharacters_WithTwoMostVideoGameAppeances_ShouldReturnTwoCharacters()
    {
        //TODO:
    }
}

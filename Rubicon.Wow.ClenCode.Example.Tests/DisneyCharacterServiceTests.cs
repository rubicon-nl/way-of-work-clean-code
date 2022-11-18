using FluentAssertions;
using Rubicon.Wow.CleanCode.Data;
using Rubicon.Wow.CleanCode.Example.Domain;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Rubicon.Wow.CleanCode.Example.Tests;

public class DisneyCharacterServiceTests
{   
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

    [Fact]
    public void GetTopDisneyCharacters_WithTwoMostMovieAppeances_ShouldReturnTwoCharacters()
    {
        //TODO:
    }

    [Fact]
    public void GetTopDisneyCharacters_WithTwoMostVideoGameAppeances_ShouldReturnTwoCharacters()
    {
        //TODO:
    }
}

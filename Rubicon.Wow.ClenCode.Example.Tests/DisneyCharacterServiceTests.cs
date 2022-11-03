using FluentAssertions;
using Microsoft.VisualBasic;
using Moq;
using Rubicon.Wow.CleanCode.Data;
using Rubicon.Wow.CleanCode.Example.Tests.TestData;
using Rubicon.Wow.CleanCode.Example.Tests.TestSupport;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Rubicon.Wow.CleanCode.Example.Tests;

public class DisneyCharacterServiceTests
{    
    // const string data = @"{"Data"....
    private readonly TestOutputWriter _outputWriter;

    public DisneyCharacterServiceTests(ITestOutputHelper output)
    {        
        _outputWriter = new TestOutputWriter(output);
    }

    [Fact]
    public async Task FetchCharacters_ShouldRetrieveCharacters()
    {
        // Arrange
        var mickeyMouse = CharacterBuilder.Create(1, "Mickey Mouse")
            .PlayedInMovies("Mickey Mouse", "THE THREE MUSKETEERS", "Fanatasia 200", "World of Illusion").Build();
        var donaldDuck = CharacterBuilder.Create(1, "Donald Duck")
            .PlayedInMovies("Donald and Pluto ", "Self Control", "World of Illusion").Build();

        DisneyCharacters characters = new DisneyCharacters { Count = 2, TotalPages = 1, NextPage = null, Data = (new DisneyCharacter[] { mickeyMouse, donaldDuck }).ToList() };
        var httpResponseContent = await JsonSerialization.SerializeAsync(characters);

        HttpClientDecorator httpClientDecorator = new HttpClientDecorator(HttpFactoryStub.GetHttpClientFactory(httpResponseContent));
        DisneyCharacterService service = new DisneyCharacterService(httpClientDecorator, _outputWriter);

        // Act
        bool resultOfService = await service.FetchCharactersAsync();

        // Assert
        Assert.True(resultOfService);
        _outputWriter.WriteLine(mickeyMouse.ToString());
        _outputWriter.WriteLine(service.DisneyCharacters.First().ToString());
        Assert.Contains(service.DisneyCharacters, dchar => dchar == mickeyMouse);

    }

    [Fact]
    public async Task GetTopDisneyCharactersWithMostMovieAppeance_OnlyTwoOfThree_ShouldReturnAsTopMost()
    {
        // Arrange
        Mock<IHttpClientDecorator> httpClientDecoratorMock = new Mock<IHttpClientDecorator>(MockBehavior.Strict);
        Mock<HttpClient> httpMessageInvokerMock = new Mock<HttpClient>();
        httpClientDecoratorMock.Setup(_ => _.Create(It.Is<string>(_ => _ == "Disney"))).Returns(httpMessageInvokerMock.Object);
        var mickeyMouse = CharacterBuilder.Create(1, "Mickey Mouse")
            .PlayedInMovies("Mickey Mouse", "THE THREE MUSKETEERS", "Fanatasia 200", "World of Illusion").Build();
        var donaldDuck = CharacterBuilder.Create(1, "Donald Duck")
            .PlayedInMovies("Donald and Pluto ", "Self Control", "World of Illusion").Build();
        var goofy = CharacterBuilder.Create(1, "Goofy")
            .PlayedInMovies("World of Illusion").Build();

        var retrievedChars = new DisneyCharacter[] { mickeyMouse, goofy, donaldDuck };
        
        DisneyCharacterService service = new DisneyCharacterService(httpClientDecoratorMock.Object, _outputWriter);
        service.SetCharacterList(retrievedChars);

        // Act
        var top2 = service.GetTopDisneyCharactersWithMostMovieAppeances(2);

        // Assert
        top2.Should().Equal(new List<DisneyCharacter> { mickeyMouse, donaldDuck });

        // Verify
        httpClientDecoratorMock.VerifyAll();

    }

    [Fact]
    public void GetTopDisneyCharactersWithMostMovieAppeances()
    {
        //TODO:
    }

    [Fact]
    public void GetTopDisneyCharactersWithMostVideoGameAppeances()
    {
        //TODO:
    }
}

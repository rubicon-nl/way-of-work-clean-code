using Moq;
using Rubicon.Wow.CleanCode.Data;
using Rubicon.Wow.CleanCode.Example.Domain;
using Rubicon.Wow.CleanCode.Example.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Rubicon.Wow.CleanCode.Example.Tests;
public class DisneyCharacterRepositoryTests
{
    // const string data = @"{"Data"....
    private readonly TestOutputWriter _outputWriter;

    public DisneyCharacterRepositoryTests(ITestOutputHelper output)
    {
        _outputWriter = new TestOutputWriter(output);
    }
      
    public async Task<HttpClientDecorator> GetHttpDecorator(DisneyCharacters responseData)
    {
        var httpResponseContent = await JsonSerialization.SerializeAsync(responseData);
        return new HttpClientDecorator(GetHttpClientFactory(httpResponseContent));
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

        var httpDecorator = await GetHttpDecorator(characters);
        DisneyCharacterRepository disneyCharacterRepository = new DisneyCharacterRepository(httpDecorator, _outputWriter);

        // Act
        var resultOfService = await disneyCharacterRepository.GetAsync();

        // Assert        
        Assert.Contains(disneyCharacterRepository.DisneyCharacters, dchar => dchar == mickeyMouse);
    }

    public IHttpClientFactory GetHttpClientFactory(string httpResponseContent)
    {
        var clientHandlerStub = new DelegatingHandlerStub();
        clientHandlerStub.ExpectedResult = httpResponseContent;
        clientHandlerStub.ContentType = "application/json";

        var client = new HttpClient(clientHandlerStub);
        client.BaseAddress = new Uri("https://api.disneyapi.dev/");
        Mock<IHttpClientFactory> mockFactory = new();
        mockFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client);
        IHttpClientFactory factory = mockFactory.Object;

        return factory;
    }
}

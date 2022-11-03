using Moq;
using Rubicon.Wow.CleanCode.Example;
using Rubicon.Wow.CleanCode.Example.Tests.TestData;
using Rubicon.Wow.CleanCode.Example.Tests.TestSupport;
using System;
using System.Linq;
using System.Net.Http;
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
    public async Task ExecuteAsync_ShouldCallProcessingFunctions_ReturnsTrue()
    {
        // Arrange
        var characterTarzan = CharacterBuilder.Create(1, "Tarzan").Build();
        var characterJane = CharacterBuilder.Create(1, "Jane").Build();
        var characters = new[] { characterTarzan, characterJane };

        Mock<IDisneyCharacterService> charServiceMock = new Mock<IDisneyCharacterService>(MockBehavior.Strict);
        charServiceMock.Setup(_ => _.FetchCharactersAsync()).Returns(Task.FromResult(true));
        charServiceMock.Setup(_ => _.GetMostFavoriteAllies(It.IsAny<int>())).Returns(characters.Select(_=>_.Name).ToList());
        charServiceMock.Setup(_=>_.GetTopDisneyCharactersWithMostVideoGameAppeances(It.IsAny<int>())).Returns(characters.ToList());
        charServiceMock.Setup(_ => _.GetTopDisneyCharactersWithMostMovieAppeances(It.IsAny<int>())).Returns(characters.ToList());
        
        ConsoleTask task = new(charServiceMock.Object, _outputWriter);

        // Act
        var result = await task.ExecuteAsync();

        // Assert        
        Assert.True(result);

        // Verify all
        charServiceMock.VerifyAll();
    }  
}
using Moq;
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
        IMock<IDisneyCharacterService> charService = new Mock<IDisneyCharacterService>();
        ConsoleTask task = new(charService.Object, _outputWriter);

        // Act
        var result = await task.ExecuteAsync();

        // Assert        
        Assert.True(result);
    }
}
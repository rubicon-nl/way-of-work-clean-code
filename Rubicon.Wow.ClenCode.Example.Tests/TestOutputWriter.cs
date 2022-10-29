using Rubicon.Wow.CleanCode.Example;
using Xunit.Abstractions;

namespace Rubicon.Wow.CleanCode.Example.Tests;
public class TestOutputWriter : IOutputWriter
{
    private readonly ITestOutputHelper _output;

    public TestOutputWriter(ITestOutputHelper output)
    {
        _output = output;
    }

    public void WriteLine(string message, params string[] parameters) => _output.WriteLine(message, parameters);
    public void WriteLine(string message) => _output.WriteLine(message);
    public void WriteLine() => _output.WriteLine("--EMPTY --");
}

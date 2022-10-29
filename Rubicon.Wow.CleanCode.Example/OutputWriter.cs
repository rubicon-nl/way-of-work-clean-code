namespace Rubicon.Wow.CleanCode.Example;
public class OutputWriter : IOutputWriter
{
    public void WriteLine(string message, params string[] parameters)
    {
        Console.WriteLine(message, parameters);
    }

    public void WriteLine() => Console.WriteLine();
}



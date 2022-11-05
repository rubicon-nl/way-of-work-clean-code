namespace Rubicon.Wow.CleanCode.Example;

public interface IOutputWriter
{
    void WriteLine(string message, params string[] parameters);
    void WriteLine(string message);
    void WriteLine();
}
namespace Gi.Domain;

public struct Line
{
    public Line(string line)
    {
        Problems = new List<string>();
        Register = "";
        IsValid = false;
        Content = Array.Empty<string>();

        if (!line.StartsWith("|"))
            Problems = Problems.Append("Problema de abertura da linha");

        if (!line.EndsWith("|"))
            Problems = Problems.Append("Problema de fechamento de linha");

        if (Problems.Any())
            return;

        var content = line.Split("|");

        if (content[1].Length != 4)
        {
            Problems = Problems.Append("Problema de quantidade de caracteres no registro");
            return;
        }

        Register = content[1];
        IsValid = !Problems.Any();
        Content = content[2..^1];
    }

    public string Register { get; }
    public string[] Content { get; }
    public bool IsValid { get; }
    public IEnumerable<string> Problems { get; }
}
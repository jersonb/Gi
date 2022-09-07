namespace Gi.Domain;

public struct Line
{
    public Line(string line)
    {
        Problems = new List<string>();
        Register = "";
        IsValid = false;
        Content = new Dictionary<int, string>();

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
  
        var position =1;
        foreach (var item in content[2..^1])
            Content.Add(position++, item);
        
    }
    public static implicit operator Line(string line)
        => new Line(line);
    public string Register { get; }
    public Dictionary<int,string> Content { get; }
    public bool IsValid { get; }
    public IEnumerable<string> Problems { get; }
}
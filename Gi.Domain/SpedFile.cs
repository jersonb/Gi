namespace Gi.Domain;

public struct SpedFile
{
    public SpedFile(IEnumerable<string> lines)
    {
        Lines = new();
        var lineCount = 1;

        foreach (var readedLine in lines)
        {
            if (string.IsNullOrEmpty(readedLine))
                continue;

            Line line = readedLine;

            if (!line.IsValid)
                throw new ArgumentException($"Linha {lineCount}: {string.Join(", ", line.Problems)}");

            Lines.Add(lineCount++, line);
        }
    }
    public Dictionary<int, Line> Lines { get; }
}
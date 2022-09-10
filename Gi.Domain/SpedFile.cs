using Gi.Domain.Readers;

namespace Gi.Domain;

public class SpedFile
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

        TaxAssessmentReader = new TaxAssessmentReader(Lines);
    }

    public Dictionary<int, Line> Lines { get; }

    public TaxAssessmentReader TaxAssessmentReader { get; }
}
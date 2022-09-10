using Gi.Domain.Readers;

namespace Gi.Domain;

public class SpedFile
{
    public SpedFile(IEnumerable<string> lines)
    {
        var readedLines = new Dictionary<int, Line>();
        var lineCount = 1;

        foreach (var readedLine in lines)
        {
            if (string.IsNullOrEmpty(readedLine))
                continue;

            Line line = readedLine;

            if (!line.IsValid)
                throw new ArgumentException($"Linha {lineCount}: {string.Join(", ", line.Problems)}");

            readedLines.Add(lineCount++, line);
        }

        TaxAssessments = new TaxAssessmentsItemsReader(readedLines);
        Invoices = new InvoicesReader(readedLines);
    }

    public TaxAssessmentsItemsReader TaxAssessments { get; }

    public InvoicesReader Invoices { get; }
}
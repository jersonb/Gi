using Gi.Domain.Models;

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

        AllTaxAssessment = Lines
         .Where(l => l.Value.Register == RegisterName._8515)
         .Select(l => new TaxAssessment(l, Lines));

        IncentivizedItems = from taxAssessment in AllTaxAssessment
                            from item in taxAssessment.Items
                            where taxAssessment.IsIncentivized
                            select item;


        NonIncentivizedItems = from taxAssessment in AllTaxAssessment
                               from item in taxAssessment.Items
                               where !taxAssessment.IsIncentivized
                               select item;
    }
    public Dictionary<int, Line> Lines { get; }
    public IEnumerable<TaxAssessment> AllTaxAssessment { get; }
    public IEnumerable<Item> IncentivizedItems { get; }
    public IEnumerable<Item> NonIncentivizedItems { get; }

}
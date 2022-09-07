namespace Gi.Domain.Models;

public class TaxAssessment
{
    public TaxAssessment(KeyValuePair<int, Line> assessment, Dictionary<int, Line> lines)
    {
        Name = assessment.Value.Content[1];
        DecreeNumber = lines[assessment.Key - 1].Content[2];
        Items = lines
           .Where(x => x.Key > assessment.Key)
           .TakeWhile(x => x.Value.Register == RegisterName._8525)
           .Select(x => new Item(x.Value));
    }

    public string Name { get; }
    public string DecreeNumber { get; }
    public IEnumerable<Item> Items { get; }
    public bool IsIncentivized => !string.IsNullOrEmpty(DecreeNumber);
}
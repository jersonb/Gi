using Gi.Domain.Models;

namespace Gi.Domain.Readers;

public class InvoicesReader
{
    public InvoicesReader(Dictionary<int, Line> lines)
    {
        var invoiceLines = lines
           .Where(line => line.Value.Register == RegisterName._8530 || line.Value.Register == RegisterName._8535)
           .OrderBy(line => line.Key)
           .Select(line => line.Value);

        Items = new List<InvoiceItem>();

        var number = string.Empty!;
        foreach (var line in invoiceLines)
        {
            if (line.Register == RegisterName._8530)
                number = line?.Content[6];
            else
                Items.Add(new InvoiceItem(line, number!));
        }
    }

    public List<InvoiceItem> Items { get; }
}
using Gi.Domain.Models;

namespace Gi.Domain.Readers;

public class InvoicesReader
{
    public InvoicesReader(Dictionary<int, Line> lines)
    {
        var invoiceLines = lines
           .Where(line => line.Value.Register == RegisterName._8530 || line.Value.Register == RegisterName._8535)
           .Reverse()
           .Select(line => line.Value);

        var invoiceItemsLines = new List<Line>();
        Invoices = new List<Invoice>();
        foreach (var line in invoiceLines)
        {
            if (line.Register == RegisterName._8530)
            {
                var invoice = new Invoice(line, invoiceLines);
                Invoices.Add(invoice);
                invoiceItemsLines = new List<Line>();
            }
            else
                invoiceItemsLines.Add(line);
        }
    }

    public List<Invoice> Invoices { get; }
}
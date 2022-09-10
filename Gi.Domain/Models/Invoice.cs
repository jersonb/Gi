namespace Gi.Domain.Models;

public class Invoice
{
    public Invoice(Line line, IEnumerable<Line> lines)
    {
        if (line.Register != RegisterName._8530)
            throw new InvalidOperationException($"Registro inadequado para cabeçalho de nota na linha |{line.Register}|{string.Join("|", line.Content)}|");

        if (!lines.Any(l => l.Register != RegisterName._8535))
            throw new InvalidOperationException($"Registro inadequado para itens da nota na linha |{line.Register}|{string.Join("|", line.Content)}|");

        Number = line.Content[6];
        InvoiceItems = lines.Select(l => new InvoiceItem(l));
    }

    public string Number { get; set; }
    public IEnumerable<InvoiceItem> InvoiceItems { get; set; }
}
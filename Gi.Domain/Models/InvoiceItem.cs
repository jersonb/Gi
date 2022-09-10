namespace Gi.Domain.Models;

public class InvoiceItem
{
    public InvoiceItem(Line line, string invoiceNumber)
    {
        if (line.Register != RegisterName._8535)
            throw new InvalidOperationException($"Registro inadequado para item de nota na linha |{line.Register}|{string.Join("|", line.Content.Select(i => i.Value))}|");

        ItemCode = line.Content[2];
        Cfop = line.Content[3];
        AmmountString = line.Content[4];

        if (!string.IsNullOrEmpty(AmmountString))
            Ammount = decimal.Parse(AmmountString);

        Aliquot = line.Content[5];

        IcmsString = line.Content[6];

        if (!string.IsNullOrEmpty(IcmsString))
            Icms = decimal.Parse(IcmsString);

        InvoiceNumber = invoiceNumber;
    }

    public string InvoiceNumber { get; set; }
    public string ItemCode { get; }
    public string Cfop { get; }
    public string AmmountString { get; }
    public decimal Ammount { get; }
    public string Aliquot { get; }
    public string IcmsString { get; }
    public decimal Icms { get; }
}
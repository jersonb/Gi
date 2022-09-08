namespace Gi.Domain.Tests.Models;

public class InvoiceTests
{
    [Fact]
    public void Test1()
    {
        var readedLines = new List<string>
        {
            "|8530|0|1|79531|55|1|000007703|22012019|1117,66|1117,66|44,71|",
            "|8535|1|1127.04.12062|2152|1117,66|4,00|44,71|1|1|",
            "|8540|1117,66|2152|1117,66|4,00|44,71|1|1|",
            "|8530|0|1|79531|55|1|000007705|22012019|2523,20|2523,20|101,70|",
            "|8535|1|125.55.965044|2151|2497,70|4,00|99,91|1|1|",
            "|8535|2|1127.02.09770|2152|21,50|7,00|1,51|1|1|",
            "|8535|3|1137.02.00385|2152|4,00|7,00|0,28|1|1|",
            "|8540|2497,70|2151|2497,70|4,00|99,91|1|1|",
            "|8540|25,50|2152|25,50|7,00|1,79|1|1|",
            "|8530|1|0|20172|55|1|000000825|28012019|220,00|220,00|26,40|",
            "|8535|1|1137.01.02844|6910|220,00|12,00|26,40|2|2|",
            "|8540|220,00|6910|220,00|12,00|26,40|2|2|",
            "|8530|1|0|20172|55|1|000000826|28012019|4855,25|0,00|0,00|",
            "|8535|1|117.01.2740442|6109||||2|2|",
            "|8535|2|117.01.279024107|6109||||2|2|",
            "|8535|3|117.01.269044M2|6109||||2|2|",
            "|8540|4855,25|6109||||2|2|",
        };

        var lines = readedLines.Select(l => new Line(l));
        var invoiceLines = lines.Where(line => line.Register == RegisterName._8530 || line.Register == RegisterName._8535).Reverse();
        
        var invoiceItemsLines = new List<Line>();
        var invoices = new List<Invoice>();
        foreach (var line in invoiceLines)
        {
            if(line.Register == RegisterName._8530)
            {
                var invoice = new Invoice(line, invoiceLines);
                invoices.Add(invoice);
                invoiceItemsLines = new List<Line>();
            }else
                invoiceItemsLines.Add(line);
        }
        Assert.Equal(17, lines.Count());
        Assert.Equal(12, invoiceLines.Count());
    }
}
public class Invoice
{
    public Invoice(Line line, IEnumerable<Line> lines)
    {
        if(line.Register != RegisterName._8530)
            throw new Exception("foi aq 1");
        if(!lines.Any(l => l.Register != RegisterName._8535))
            throw new Exception("foi aq 2");

        Number = line.Content[6];
        InvoiceItems = lines.Select(l => new InvoiceItem(l));
    }
    public string Number { get; set; }
    public IEnumerable<InvoiceItem> InvoiceItems { get; set; }
}
public class InvoiceItem
{
    public InvoiceItem(Line line)
    {
         if(line.Register != RegisterName._8535)
            throw new Exception();

        ItemCode = line.Content[2];
        Cfop = line.Content[3];
        AmmountString = line.Content[4];
        
        if(!string.IsNullOrEmpty(AmmountString))
            Ammount = decimal.Parse(AmmountString);
        
        Aliquot = line.Content[5];

        IcmsString = line.Content[6];
       
        if (!string.IsNullOrEmpty(IcmsString))
            Icms = decimal.Parse(IcmsString);
    }

    public string ItemCode { get; }
    public string Cfop { get; }
    public string AmmountString { get; }
    public decimal Ammount { get; }
    public string Aliquot { get; }
    public string IcmsString { get; }
    public decimal Icms { get; }
}
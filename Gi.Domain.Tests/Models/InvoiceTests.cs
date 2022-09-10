using Gi.Domain.Readers;

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

        var lines = readedLines
            .Select((l, i) => new KeyValuePair<int, Line>(i, new Line(l)))
            .ToDictionary(i => i.Key, v => v.Value);

        var invoices = new InvoicesReader(lines);

        Assert.Equal(17, lines.Count);
        Assert.Equal(4, invoices.Invoices.Count);
    }
}
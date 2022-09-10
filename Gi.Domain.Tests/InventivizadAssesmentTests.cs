namespace Gi.Domain.Tests;

public class InventivizadAssesmentTests
{
    [Fact]
    public void Test1()
    {
        var readedlines = new List<string>
        {
            "|8505|0||||1|",
            "|8515|1|",
            "|8525|1127.02.00985|CAIXA PAPELAO 00 350x180x220  FBC-212|48191000|PC||0000|",
            "|8525|STS-5509|BOMBAS SUBM. COMPLETAS REF. ST-5509 (SUBWELL)|84137010|PC||0000|",
            "|8525|STS-8014|BOMBAS SUBM. COMPLETAS REF. ST-8014 (SUBWELL)|84137010|PC||0000|",
            "|8505|1|41560|18032015|0|1|",
            "|8515|2|",
            "|8525|97X.01.246072|BOMBA THA-16 AL 3,0 CV MONOF.IP21.127/220-254V.|84137080|PC|0,00|0000|",
            "|8525|97X.01.9780222|BOMBA TJ-12/30 AL 1/2 CV MONOF.IP21.WPUMP-220V.|84137080|PC||0000|",
            "|8525|97X.01.9780222|BOMBA TJ-12/30 AL 1/2 CV MONOF.IP21.WPUMP-220V.|84137080|PC|75,00|0000|",
            "|8525|97X.01.9780222|BOMBA TJ-12/30 AL 1/2 CV MONOF.IP21.WPUMP-220V.|84137080|PC|75,00|0000|",
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
        var spedFile = new SpedFile(readedlines);

        var incentivizedItems = spedFile.TaxAssessments.Incentivized;
        var nonIcentivizedItems = spedFile.TaxAssessments.NonIncentivized;
        var invoiceItems = spedFile.Invoices.Items.Where(i => i.InvoiceNumber == "000007705");
        Assert.Equal(4, incentivizedItems.Count);
        Assert.Equal(3, nonIcentivizedItems.Count);
        Assert.Equal(3, invoiceItems.Count());
    }
}
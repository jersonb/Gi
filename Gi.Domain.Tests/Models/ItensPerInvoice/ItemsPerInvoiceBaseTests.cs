using Gi.Domain.Models.ItemsPerInvoice;
using Gi.Domain.Readers;
using Xunit.Abstractions;
using static Gi.Domain.Readers.InvoicesReader;

namespace Gi.Domain.Tests.Models.ItensPerInvoice;

public class ItemsPerInvoiceBaseTests
{
    private readonly ITestOutputHelper output;
    private readonly TaxAssessmentsItemsReader taxAssessments;
    private readonly InvoiceItems invoiceItems;

    public ItemsPerInvoiceBaseTests(ITestOutputHelper output)
    {
        this.output = output;
        var spedFile = new SpedFile(Readedlines);
        taxAssessments = spedFile.TaxAssessments;
        invoiceItems = spedFile.Invoices.Items;
    }

    private static List<string> Readedlines => new()
    {
        "|8505|0||||1|",
        "|8515|1|",
        "|8525|1127.02.00985|CAIXA PAPELAO 00 350x180x220  FBC-212|48191000|PC||0000|",
        "|8505|1|41560|18032015|0|1|",
        "|8515|2|",

        "|8525|97X.01.9780222|B1|84137080|PC|75,00|0000|",
        "|8525|97X.01.9780223|B2|84137080|PC|0,00|0000|",

        "|8530|0|1|79531|55|1|000007705|22012019|2523,20|2523,20|101,70|",
        "|8535|1|1127.02.00985|2151|2497,70|4,00|99,91|1|1|",
        "|8535|2|1127.02.00985|2152|21,50|7,00|1,51|1|1|",
        "|8535|3|1127.02.00985|2152|4,00|7,00|0,28|1|1|",
        "|8540|2497,70|2151|2497,70|4,00|99,91|1|1|",
        "|8540|25,50|2152|25,50|7,00|1,79|1|1|",
        "|8530|1|0|20172|55|1|000000825|28012019|221,00|221,00|27,40|",
        "|8535|1|97X.01.9780222|6910|221,00|12,00|27,40|2|2|",
        "|8535|2|97X.01.9780222|6109||||2|2|",
        "|8535|3|97X.01.9780222|6109||||2|2|",
        "|8540|220,00|6910|220,00|12,00|26,40|2|2|",

        "|8530|1|0|20172|55|1|000000826|28012019|220,00|220,00|26,40|",
        "|8535|1|97X.01.9780223|6910|220,00|12,00|26,40|2|2|",
        "|8540|220,00|6910|220,00|12,00|26,40|2|2|",
    };

    [Fact]
    public void Test1()
    {
        var incentivizedItems = taxAssessments.Incentivized;

        var incentivizedItemsPerInvoice = new IncentivizedItemsPerInvoice(incentivizedItems, invoiceItems).Base;

        foreach (var item in incentivizedItemsPerInvoice)
            output.WriteLine(item.ToString());

        Assert.Equal(7, invoiceItems.Count);
        Assert.Equal(2, incentivizedItems.Count);
        Assert.Equal(4, incentivizedItemsPerInvoice.Count());
    }

    [Fact]
    public void Test2()
    {
        var nonIncentivizedItems = taxAssessments.NonIncentivized;

        var nonIncentivizedItemsPerInvoice = new NonIncentivizedItemsPerInvoice(nonIncentivizedItems, invoiceItems).Base;

        foreach (var item in nonIncentivizedItemsPerInvoice)
            output.WriteLine(item.ToString());

        Assert.Equal(3, nonIncentivizedItemsPerInvoice.Count());
        Assert.Single(nonIncentivizedItems);
        Assert.Equal(7, invoiceItems.Count);
    }

    [Fact]
    public void Test3()
    {
        var nonIncentivizedItems = taxAssessments.NonIncentivized;

        var perCfopAndAliquot = new NonIncentivizedItemsPerInvoice(nonIncentivizedItems, invoiceItems).CfopAliquot;

        foreach (var item in perCfopAndAliquot)
            output.WriteLine(item.ToString());

        Assert.Equal(2, perCfopAndAliquot.Count());
    }

    [Fact]
    public void Test4()
    {
        var incentivizedItems = taxAssessments.Incentivized;

        var perItemAndNcm = new IncentivizedItemsPerInvoice(incentivizedItems, invoiceItems).ItemNcm;

        foreach (var item in perItemAndNcm)
            output.WriteLine(item.ToString());

        Assert.Equal(2, perItemAndNcm.Count());
    }

    [Fact]
    public void Test5()
    {
        var incentivizedItems = taxAssessments.Incentivized;

        var perItemInvoiceCfop = new IncentivizedItemsPerInvoice(incentivizedItems, invoiceItems).ItemInvoiceCfop;

        foreach (var item in perItemInvoiceCfop)
            output.WriteLine(item.ToString());

        Assert.Equal(3, perItemInvoiceCfop.Count());
    }

    [Fact]
    public void Test6()
    {
        var incentivizedItems = taxAssessments.Incentivized;

        var perNcmCfop = new IncentivizedItemsPerInvoice(incentivizedItems, invoiceItems).NcmCfop;

        foreach (var item in perNcmCfop)
            output.WriteLine(item.ToString());

        Assert.Equal(2, perNcmCfop.Count());
    }
}
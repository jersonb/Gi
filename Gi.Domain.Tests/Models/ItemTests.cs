namespace Gi.Domain.Tests.Models;

public class ItemTests
{
     [Fact]
    public void Tets1()
    {
        var readedLine = "|8525|1127.02.00985|CAIXA PAPELAO 00 350x180x220  FBC-212|48191000|PC||0000|";
        var line = new Line(readedLine);
        var item = new Item(line);
        Assert.Equal( "1127.02.00985", item.Code);
        Assert.Equal( "CAIXA PAPELAO 00 350x180x220  FBC-212", item.Name);
        Assert.Equal( "48191000", item.NcmCode);
        Assert.Equal( "PC", item.Unit);
        Assert.Equal( "0,00", item.PercentageIncentivized);
    }
}
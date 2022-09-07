namespace Gi.Domain.Tests;

public class SpedFileTests
{
    [Fact]
    public void Test1()
    {
        var lines = new List<string>
        {
            "|0000|L01C01|L01C02|L01C03|L01C04|",
            "|0001|L02C01|L02C02|L02C03|L02C04|",
            ""
        };
        var spedFile = new SpedFile(lines);

        Assert.Equal(2, spedFile.Lines.Count);
        Assert.Equal(1, spedFile.Lines.First().Key);
    }
   
    [Fact]
    public void Test2()
    {
        var lines = new List<string>
        {
            "|0000|L01C01|L01C02|L01C03|L01C04|",
            "0001|L02C01|L02C02|L02C03|L02C04|",
            ""
        };
        var ex = Assert.Throws<ArgumentException>(() => new SpedFile(lines));
        Assert.Equal("Linha 2: Problema de abertura da linha", ex.Message);
    }
   
    [Fact]
    public void Test3()
    {
        var lines = new List<string>
        {
            "|0000|L01C01|L01C02|L01C03|L01C04|",
            "|000|L02C01|L02C02|L02C03|L02C04|",
            ""
        };
        var ex = Assert.Throws<ArgumentException>(() => new SpedFile(lines));
        Assert.Equal("Linha 2: Problema de quantidade de caracteres no registro", ex.Message);
    }
   
    [Fact]
    public void Test4()
    {
        var lines = new List<string>
        {
            "|0000|L01C01|L01C02|L01C03|L01C04|",
            "|0000|L02C01|L02C02|L02C03|L02C04",
            ""
        };
        var ex = Assert.Throws<ArgumentException>(() => new SpedFile(lines));
        Assert.Equal("Linha 2: Problema de fechamento de linha", ex.Message);
    }
}


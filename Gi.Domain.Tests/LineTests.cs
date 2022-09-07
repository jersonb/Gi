namespace Gi.Domain.Tests;

public class LineTests
{

    [Fact]
    public void Test1()
    {
        var readedLine = "|0000|L01C01|L01C02|L01C03|L01C04|";
        var line = new Line(readedLine);

        Assert.True(line.IsValid);
        Assert.Equal(4, line.Content.Length);
        Assert.Equal("0000", line.Register);
        Assert.Equal("L01C01", line.Content.First());
        Assert.Equal("L01C04", line.Content.Last());
    }

    [Fact]
    public void Test2()
    {
        var readedLine = "|000|L01C01|L01C02|L01C03|L01C04|";
        var line = new Line(readedLine);

        Assert.False(line.IsValid);
        Assert.Single(line.Problems);
        Assert.Contains("Problema de quantidade de caracteres no registro", line.Problems);
    }

     [Fact]
    public void Test3()
    {
        var readedLine = "0000|L01C01|L01C02|L01C03|L01C04|";
        var line = new Line(readedLine);

        Assert.False(line.IsValid);
        Assert.Single(line.Problems);
        Assert.Contains("Problema de abertura da linha", line.Problems);
    }

     [Fact]
    public void Test4()
    {
        var readedLine = "|0000|L01C01|L01C02|L01C03|L01C04";
        var line = new Line(readedLine);

        Assert.False(line.IsValid);
        Assert.Single(line.Problems);
        Assert.Contains("Problema de fechamento de linha", line.Problems);
    }
}
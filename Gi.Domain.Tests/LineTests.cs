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
}
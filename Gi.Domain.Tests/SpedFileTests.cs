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
}


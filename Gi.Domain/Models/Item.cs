namespace Gi.Domain.Models;
public class Item
{
    public Item(Line line)
    {
        var contents = line.Content;
        Code = contents[1];
        Name = contents[2];
        NcmCode = contents[3];
        Unit = contents[4];
        PercentageIncentivized = string.IsNullOrEmpty(contents[5]) ? "0,00" : contents[5];

    }

    public string Code { get; }
    public string Name { get; }
    public string NcmCode { get; }
    public string Unit { get; set; }
    public string PercentageIncentivized { get; }
}

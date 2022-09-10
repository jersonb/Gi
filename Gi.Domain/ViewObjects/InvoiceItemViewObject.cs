namespace Gi.Domain.ViewObjects;

public class InvoiceItemViewObject
{
    public string Code { get; set; } = string.Empty!;
    public string Name { get; set; } = string.Empty!;
    public string NcmCode { get; set; } = string.Empty!;
    public string PercentageIncentivized { get; set; } = string.Empty!;
    public string Invoice { get; set; } = string.Empty!;
    public string Cfop { get; set; } = string.Empty!;
    public string Aliquot { get; set; } = string.Empty!;
    public decimal Ammount { get; set; }
    public decimal Icms { get; set; }

    public override string? ToString()
    {
        var list = new List<string>();

        if (!string.IsNullOrEmpty(Code))
            list.Add(Code);

        if (!string.IsNullOrEmpty(Name))
            list.Add(Name);

        if (!string.IsNullOrEmpty(NcmCode))
            list.Add(NcmCode);

        if (!string.IsNullOrEmpty(PercentageIncentivized))
            list.Add(PercentageIncentivized);

        if (!string.IsNullOrEmpty(Invoice))
            list.Add(Invoice);

        if (!string.IsNullOrEmpty(Cfop))
            list.Add(Cfop);

        if (!string.IsNullOrEmpty(Aliquot))
            list.Add(Aliquot);

        list.Add(Ammount.ToString("F2"));
        list.Add(Icms.ToString("F2"));
        return $"|{string.Join("|", list)}|";
    }
}
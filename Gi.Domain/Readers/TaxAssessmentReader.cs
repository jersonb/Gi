using Gi.Domain.Models;

namespace Gi.Domain.Readers;

public class TaxAssessmentReader
{
    public TaxAssessmentReader(Dictionary<int, Line> lines)
    {
        AllTaxAssessment = lines
         .Where(l => l.Value.Register == RegisterName._8515)
         .Select(l => new TaxAssessment(l, lines));

        IncentivizedItems = from taxAssessment in AllTaxAssessment
                            from item in taxAssessment.Items
                            where taxAssessment.IsIncentivized
                            select item;


        NonIncentivizedItems = from taxAssessment in AllTaxAssessment
                               from item in taxAssessment.Items
                               where !taxAssessment.IsIncentivized
                               select item;
    }

    public IEnumerable<TaxAssessment> AllTaxAssessment { get; }
    public IEnumerable<Item> IncentivizedItems { get; }
    public IEnumerable<Item> NonIncentivizedItems { get; }
}
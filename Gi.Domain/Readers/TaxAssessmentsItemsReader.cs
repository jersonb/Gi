using Gi.Domain.Models;

namespace Gi.Domain.Readers;

public class TaxAssessmentsItemsReader
{
    public TaxAssessmentsItemsReader(Dictionary<int, Line> lines)
    {
        All = lines
         .Where(l => l.Value.Register == RegisterName._8515)
         .Select(l => new TaxAssessment(l, lines));

        Incentivized = new IncentivizedItems(All);

        NonIncentivized = new NonIncentivizedItems(All);
    }

    public IEnumerable<TaxAssessment> All { get; }
    public IncentivizedItems Incentivized { get; }
    public NonIncentivizedItems NonIncentivized { get; }

    public class IncentivizedItems : List<Item>
    {
        public IncentivizedItems(IEnumerable<TaxAssessment> taxAssessments)
        {
            var items = from taxAssessment in taxAssessments
                        from item in taxAssessment.Items
                        where taxAssessment.IsIncentivized
                        select item;
            AddRange(items);
        }
    }

    public class NonIncentivizedItems : List<Item>
    {
        public NonIncentivizedItems(IEnumerable<TaxAssessment> taxAssessments)
        {
            var items = from taxAssessment in taxAssessments
                        from item in taxAssessment.Items
                        where !taxAssessment.IsIncentivized
                        select item;
            AddRange(items);
        }
    }
}
using Gi.Domain.Readers;
using static Gi.Domain.Readers.TaxAssessmentsItemsReader;

namespace Gi.Domain.Models.ItemsPerInvoice
{
    public sealed class NonIncentivizedItemsPerInvoice : ItemsPerInvoiceBase
    {
        public NonIncentivizedItemsPerInvoice(NonIncentivizedItems items, InvoicesReader.InvoiceItems invoiceItems) : base(items, invoiceItems)
        {
        }
    }
}
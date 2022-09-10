using static Gi.Domain.Readers.InvoicesReader;
using static Gi.Domain.Readers.TaxAssessmentsItemsReader;

namespace Gi.Domain.Models.ItemsPerInvoice;

public sealed class IncentivizedItemsPerInvoice : ItemsPerInvoiceBase
{
    public IncentivizedItemsPerInvoice(IncentivizedItems items, InvoiceItems invoiceItems) : base(items, invoiceItems)
    {
    }
}


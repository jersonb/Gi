using Gi.Domain.ViewObjects;
using static Gi.Domain.Readers.InvoicesReader;

namespace Gi.Domain.Models.ItemsPerInvoice
{
    public abstract class ItemsPerInvoiceBase
    {
        protected ItemsPerInvoiceBase(List<Item> items, InvoiceItems invoiceItems)
        {
            Base =
            from incentivizedItem in items
            join invoiceItem in invoiceItems on incentivizedItem.Code equals invoiceItem.ItemCode
            select new InvoiceItemViewObject
            {
                Code = incentivizedItem.Code,
                Name = incentivizedItem.Name,
                NcmCode = incentivizedItem.NcmCode,
                PercentageIncentivized = incentivizedItem.PercentageIncentivized,
                Invoice = invoiceItem.InvoiceNumber,
                Cfop = invoiceItem.Cfop,
                Aliquot = invoiceItem.Aliquot,
                Ammount = invoiceItem.Ammount,
                Icms = invoiceItem.Icms,
            };

            CfopAliquot = Base
            .GroupBy(item => new { item.Cfop, item.Aliquot })
            .Select(item => new InvoiceItemViewObject
            {
                Cfop = item.Key.Cfop,
                Aliquot = item.Key.Aliquot,
                Ammount = item.Sum(i => i.Ammount),
                Icms = item.Sum(i => i.Icms)
            });

            ItemNcm = Base
            .GroupBy(item => new { item.Code, item.Name, item.NcmCode })
            .Select(item => new InvoiceItemViewObject
            {
                Code = item.Key.Code,
                Name = item.Key.Name,
                NcmCode = item.Key.NcmCode,
                Ammount = item.Sum(i => i.Ammount),
                Icms = item.Sum(i => i.Icms)
            });

            ItemInvoiceCfop = Base
            .GroupBy(item => new { item.Code, item.Name, item.NcmCode, item.Invoice, item.Cfop })
            .Select(item => new InvoiceItemViewObject
            {
                Code = item.Key.Code,
                Name = item.Key.Name,
                NcmCode = item.Key.NcmCode,
                Invoice = item.Key.Invoice,
                Cfop = item.Key.Cfop,
                Ammount = item.Sum(i => i.Ammount),
                Icms = item.Sum(i => i.Icms)
            });

            NcmCfop = Base
            .GroupBy(item => new { item.NcmCode, item.Cfop })
            .Select(item => new InvoiceItemViewObject
            {
                NcmCode = item.Key.NcmCode,
                Cfop = item.Key.Cfop,
                Ammount = item.Sum(i => i.Ammount),
                Icms = item.Sum(i => i.Icms)
            });
        }

        public IEnumerable<InvoiceItemViewObject> NcmCfop { get; }
        public IEnumerable<InvoiceItemViewObject> ItemInvoiceCfop { get; }
        public IEnumerable<InvoiceItemViewObject> ItemNcm { get; }
        public IEnumerable<InvoiceItemViewObject> CfopAliquot { get; }
        public IEnumerable<InvoiceItemViewObject> Base { get; }
    }
}
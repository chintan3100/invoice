//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace InvoiceApplication
{
    using System;
    using System.Collections.Generic;
    
    public partial class Product
    {
        public long Id { get; set; }
        public Nullable<long> InvoiceId { get; set; }
        public string Name { get; set; }
        public Nullable<int> HSN { get; set; }
        public Nullable<int> UOM { get; set; }
        public Nullable<int> Quantity { get; set; }
        public Nullable<decimal> Rate { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public Nullable<double> Discount { get; set; }
        public Nullable<decimal> TaxableValue { get; set; }
        public Nullable<double> CGSTRate { get; set; }
        public Nullable<decimal> CGSTAmount { get; set; }
        public Nullable<double> SGSTRate { get; set; }
        public Nullable<decimal> SGSTAmount { get; set; }
        public Nullable<double> IGSTRate { get; set; }
        public Nullable<decimal> IGSTAmount { get; set; }
        public Nullable<decimal> Total { get; set; }
    
        public virtual Invoice Invoice { get; set; }
    }
}

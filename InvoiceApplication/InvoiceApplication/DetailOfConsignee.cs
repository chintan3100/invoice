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
    
    public partial class DetailOfConsignee
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DetailOfConsignee()
        {
            this.Invoices = new HashSet<Invoice>();
        }
    
        public long Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string GSTIN { get; set; }
        public string State { get; set; }
        public Nullable<long> StateCode { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Invoice> Invoices { get; set; }
    }
}

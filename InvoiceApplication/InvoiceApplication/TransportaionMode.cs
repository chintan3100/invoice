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
    
    public partial class TransportaionMode
    {
        public long Id { get; set; }
        public long InvoiceId { get; set; }
        public string VechicleNo { get; set; }
        public string DateOfSupply { get; set; }
        public string PlaceOfSupply { get; set; }
    
        public virtual Invoice Invoice { get; set; }
    }
}
﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class InvoiceEntities : DbContext
    {
        public InvoiceEntities()
            : base("name=InvoiceEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<DetailOfConsignee> DetailOfConsignees { get; set; }
        public virtual DbSet<Invoice> Invoices { get; set; }
        public virtual DbSet<TransportaionMode> TransportaionModes { get; set; }
        public virtual DbSet<Product> Products { get; set; }
    }
}
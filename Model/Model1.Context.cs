﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SpaManagement.Model
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class QUANLYSPAEntities : DbContext
    {
        public QUANLYSPAEntities()
            : base("name=QUANLYSPAEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<ACCOUNT> ACCOUNTs { get; set; }
        public virtual DbSet<BOOKING> BOOKINGs { get; set; }
        public virtual DbSet<CUSTOMER> CUSTOMERs { get; set; }
        public virtual DbSet<EMPLOYEE> EMPLOYEEs { get; set; }
        public virtual DbSet<PAYMENT> PAYMENTs { get; set; }
        public virtual DbSet<PAYMENT_DETAIL_PRODUCT> PAYMENT_DETAIL_PRODUCT { get; set; }
        public virtual DbSet<PAYMENT_DETAIL_SERVICE> PAYMENT_DETAIL_SERVICE { get; set; }
        public virtual DbSet<PRODUCT> PRODUCTs { get; set; }
        public virtual DbSet<RECEIPT> RECEIPTs { get; set; }
        public virtual DbSet<RECEIPT_DETAIL> RECEIPT_DETAIL { get; set; }
        public virtual DbSet<SERVICESS> SERVICESSes { get; set; }
    }
}

//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class PAYMENT_DETAIL_SERVICE
    {
        public int PMT_ID { get; set; }
        public int S_ID { get; set; }
        public int E_ID { get; set; }
    
        public virtual EMPLOYEE EMPLOYEE { get; set; }
        public virtual PAYMENT PAYMENT { get; set; }
        public virtual SERVICESS SERVICESS { get; set; }
    }
}

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
    
    public partial class PRODUCT
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PRODUCT()
        {
            this.PAYMENT_DETAIL_PRODUCT = new HashSet<PAYMENT_DETAIL_PRODUCT>();
        }
    
        public int PRO_ID { get; set; }
        public string PRO_MA { get; set; }
        public string PRO_NAME { get; set; }
        public decimal PRICE { get; set; }
        public int IN_STOCK { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PAYMENT_DETAIL_PRODUCT> PAYMENT_DETAIL_PRODUCT { get; set; }
    }
}

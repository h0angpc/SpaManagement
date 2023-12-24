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
    using SpaManagement.ViewModel;
    using System;
    using System.Collections.Generic;

    public partial class CUSTOMER : BaseViewModel
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CUSTOMER()
        {
            this.BOOKINGs = new HashSet<BOOKING>();
            this.PAYMENTs = new HashSet<PAYMENT>();
        }

        public int CUS_ID { get; set; }
        public string CUS_MA { get; set; }
        private string _CUS_NAME;
        public string CUS_NAME { get => _CUS_NAME; set { _CUS_NAME = value; OnPropertyChanged(); } }
        private string _CUS_PHONE;
        public string CUS_PHONE { get => _CUS_PHONE; set { _CUS_PHONE = value; OnPropertyChanged(); } }
        private string _CUS_EMAIL;
        public string CUS_EMAIL { get => _CUS_EMAIL; set { _CUS_EMAIL = value; OnPropertyChanged(); } }
        private string _CUS_SEX;
        public string CUS_SEX { get => _CUS_SEX; set { _CUS_SEX = value; OnPropertyChanged(); } }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BOOKING> BOOKINGs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PAYMENT> PAYMENTs { get; set; }
    }
}
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
    
    public partial class PRODUCT:BaseViewModel
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PRODUCT()
        {
            this.PAYMENT_DETAIL_PRODUCT = new HashSet<PAYMENT_DETAIL_PRODUCT>();
        }

        public int PRO_ID { get; set; }
        public string PRO_MA { get; set; }

        private string _PRO_NAME;
        public string PRO_NAME 
        {
            get => _PRO_NAME;
            set
            {
                _PRO_NAME = value;
                OnPropertyChanged();
            }
        }

        private string _PRO_IMG;
        public string PRO_IMG 
        {
            get => _PRO_IMG;
            set
            {
                _PRO_IMG = value;
                OnPropertyChanged();
            }
        }

        private string _PRO_URL;
        public string PRO_URL 
        {
            get => _PRO_URL;
            set
            {
                _PRO_URL = value;
                OnPropertyChanged();
            }
        }

        private decimal _PRICE;
        public decimal PRICE { 
            get => _PRICE; 
            set
            {
                _PRICE = value;
                OnPropertyChanged();
            }
        }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PAYMENT_DETAIL_PRODUCT> PAYMENT_DETAIL_PRODUCT { get; set; }
    }
}

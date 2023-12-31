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
    
    public partial class EMPLOYEE:BaseViewModel
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public EMPLOYEE()
        {
            this.BOOKINGs = new HashSet<BOOKING>();
        }

        public int EMP_ID { get; set; }

        public string EMP_MA { get; set; }

        private string _EMP_DISPLAYNAME;
        public string EMP_DISPLAYNAME
        {
            get => _EMP_DISPLAYNAME;
            set
            {
                _EMP_DISPLAYNAME = value;
                OnPropertyChanged();
            }
        }

        private string _EMP_ADDRESS;
        public string EMP_ADDRESS
        {
            get => _EMP_ADDRESS;
            set
            {
                _EMP_ADDRESS = value;
                OnPropertyChanged();
            }
        }

        private string _EMP_PHONE;
        public string EMP_PHONE
        {
            get => _EMP_PHONE;
            set
            {
                _EMP_PHONE = value;
                OnPropertyChanged();
            }
        }

        private string _EMP_CCCD;
        public string EMP_CCCD
        {
            get => _EMP_CCCD;
            set
            {
                _EMP_CCCD = value;
                OnPropertyChanged();
            }
        }

        private decimal _EMP_SALARY;
        public decimal EMP_SALARY
        {
            get => _EMP_SALARY;
            set
            {
                _EMP_SALARY = value;
                OnPropertyChanged();
            }
        }

        private string _EMP_ROLE;
        public string EMP_ROLE
        {
            get => _EMP_ROLE;
            set
            {
                _EMP_ROLE = value;
                OnPropertyChanged();
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BOOKING> BOOKINGs { get; set; }
    }
}

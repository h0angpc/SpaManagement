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

    public partial class PAYMENT_DETAIL_PRODUCT : BaseViewModel
    {
        public int PMT_ID { get; set; }
        public int P_ID { get; set; }

        private int _QUANTITY;
        public int QUANTITY
        {
            get => _QUANTITY;
            set
            {
                _QUANTITY = value;
                OnPropertyChanged();
            }
        }

        private decimal _AMOUNT;
        public decimal AMOUNT
        {
            get => _AMOUNT;
            set
            {
                _AMOUNT = value;
                OnPropertyChanged();
            }
        }

        public virtual PAYMENT PAYMENT { get; set; }
        public virtual PRODUCT PRODUCT { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using SpaManagement.Model;
using SpaManagement.Views;
namespace SpaManagement.ViewModel
{
    public class ServiceViewModel : BaseViewModel
    {

       
        private ObservableCollection<SERVICESS> _ServiceList;
        public ObservableCollection<SERVICESS> ServiceList { get => _ServiceList; set { _ServiceList = value; OnPropertyChanged(); } }
        public ICommand ShowAddSerCommand { get; set; }
        public ICommand ShowEditSerCommand { get; set; }
        public ICommand RemoveSerCommand { get; set; }
        private ICollectionView _servicecollection;
        public ICollectionView ServiceCollection
        {
            get { return _servicecollection; }
            set { _servicecollection = value; OnPropertyChanged(); }
        }

        private string _textToFilter;

        public string TextToFilter
        {
            get { return _textToFilter; }
            set
            {
                _textToFilter = value;
                OnPropertyChanged(nameof(TextToFilter));
                ServiceCollection.Filter = FilterByName;
            }
        }

        private SERVICESS _SelectedService;
        public SERVICESS SelectedService
        {
            get => _SelectedService;
            set
            {
                _SelectedService = value;
                OnPropertyChanged();
            }
        }


        public ServiceViewModel()
        {
            _ServiceList = ServiceManager.GetService();
            ServiceCollection = CollectionViewSource.GetDefaultView(_ServiceList);


            ShowAddSerCommand = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) =>
            {
                AddServiceView wd = new AddServiceView();
                wd.ShowDialog();
            });
            


            ShowEditSerCommand = new RelayCommand<SERVICESS>((p) =>
            {
                if (SelectedService == null)
                    return false;
                return true;
            },
            (p) =>
            {
                EditServiceViewModel editServiceViewModel = new EditServiceViewModel(SelectedService);
                EditServiceView wd = new EditServiceView();
                wd.DataContext = editServiceViewModel;
                wd.ShowDialog();

            });

            RemoveSerCommand = new RelayCommand<SERVICESS>((p) =>
            {
                if (SelectedService == null)
                    return false;
                return true;
            },
            (p) =>
            {
                try
                {
                    //if (p !=null)
                    //{
                    //    PAYMENT_DETAIL_SERVICE pay = DataProvider.Ins.DB.PAYMENT_DETAIL_SERVICE.FirstOrDefault(x => x.S_ID == p.SER_ID);
                    //    BOOKING book = DataProvider.Ins.DB.BOOKINGs.FirstOrDefault(x=> x.S_ID == p.SER_ID);

                    //    if (pay!=null || book != null)
                    //    {
                    //        MessageBoxCustom m = new MessageBoxCustom("Không thể xóa dịch vụ này vì tồn tại nhiều dữ liệu liên quan", MessageType.Info, MessageButtons.Ok);
                    //        m.ShowDialog();
                    //    }
                    //    else
                    //    {
                    //        bool? result = new MessageBoxCustom("Xác nhận xóa dịch vụ?", MessageType.Confirmation, MessageButtons.YesNo).ShowDialog();
                    //        if (result.Value)
                    //        {
                    //            ServiceManager.RemoveServcie(p);
                    //            DataProvider.Ins.DB.SERVICESSes.Remove(p);

                    //            DataProvider.Ins.DB.SaveChanges();
                    //            MessageBoxCustom m = new MessageBoxCustom("Xóa dịch vụ thành công!", MessageType.Info, MessageButtons.Ok);
                    //            m.ShowDialog();
                    //        }
                    //        else
                    //        {
                    //            return;
                    //        }
                    //    }
                    //}
                    bool? result = new MessageBoxCustom("Xác nhận xóa dịch vụ?", MessageType.Confirmation, MessageButtons.YesNo).ShowDialog();
                    if (result.Value)
                    {
                        var pro = DataProvider.Ins.DB.SERVICESSes.FirstOrDefault(x => x.SER_ID == SelectedService.SER_ID);
                        pro.IS_DELETED = true;

                        ServiceManager.RemoveServcie(SelectedService);
                        DataProvider.Ins.DB.SaveChanges();
                        MessageBoxCustom m = new MessageBoxCustom("Xóa sản phẩm thành công!", MessageType.Info, MessageButtons.Ok);
                        m.ShowDialog();
                    }
                    else
                    {
                        return;
                    }
                }
                catch (Exception ex)
                {
                    MessageBoxCustom m = new MessageBoxCustom(ex.Message, MessageType.Info, MessageButtons.Ok);
                    m.ShowDialog();
                }

            });
        }
        private bool FilterByName(object ser)
        {
            if (!string.IsNullOrEmpty(TextToFilter))
            {
                var serDetail = ser as SERVICESS;
                if (serDetail != null)
                {
                    string filtertext = TextToFilter.ToLower();
                    string serviceName = serDetail.SER_NAME.ToLower();

                    return serviceName.Contains(filtertext);
                }
            }
            return true;
        }


    }
}

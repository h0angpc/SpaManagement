using SpaManagement.Model;
using SpaManagement.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Net;
using System.Net.Mail;

namespace SpaManagement.ViewModel
{
    public class QuenMKViewModel: BaseViewModel
    {
        public ICommand CloseCommand { get; set; }
        public ICommand QuenMK { get; set; }

        public string _email;
        public string email { get => _email; set { _email = value; OnPropertyChanged(); } }
        public QuenMKViewModel() 
        {
            CloseCommand = new RelayCommand<Window>((p) => { return p == null ? false : true; }, (p) => {
                var w = (p);
                if (w != null)
                {
                    w.Close();
                }
            });

            QuenMK = new RelayCommand<object>((p) =>
            {
                //nếu email chưa được điền thì không kích hoạt
                if (email ==null)
                    return false;
                //nếu email điền vào không tồn tại trong Database thì không kích hoạt
                var displayList = DataProvider.Ins.DB.ACCOUNTs.Where(x => x.A_EMAIL==email);
                if (displayList == null || displayList.Count() == 0)
                    return false;
                return true;
            }, (p) =>
            {
                string password = "";
                var accout = DataProvider.Ins.DB.ACCOUNTs.Where(x => x.A_EMAIL == email).FirstOrDefault();
                password = ToBase64Decode(accout.A_PASSWORD);

                //string from = "hba6969692000@gmail.com";
                //string passfrom = "@hba123456";

                string fromEmail = "hba6969692000@gmail.com";
                string passwordfrom = "rnyqxjtjykvxcmyp";

                // Tạo đối tượng MailMessage
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(fromEmail);
                mail.To.Add(new MailAddress(email));
                mail.Subject = "LẤY LẠI MẬT KHẨU";
                mail.Body =  "<html><body> Mật khẩu của bạn là: " + password + "</body></html>";
                mail.IsBodyHtml = true;

                // Cấu hình client SMTP
                var smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential(fromEmail, passwordfrom),
                    EnableSsl = true,
                };
                try
                {
                    smtpClient.Send(mail);
                    MessageBoxCustom m = new MessageBoxCustom("Gửi mail thành công", MessageType.Info, MessageButtons.Ok);
                    m.ShowDialog();
                }
                catch (Exception ex)
                {
                    MessageBoxCustom m = new MessageBoxCustom("Lỗi" + ex.Message, MessageType.Error, MessageButtons.Ok);
                    m.ShowDialog();
                }


            });

        }

        public static string ToBase64Decode(string base64EncodedText)
        {
            if (String.IsNullOrEmpty(base64EncodedText))
            {
                return base64EncodedText;
            }

            byte[] base64EncodedBytes = Convert.FromBase64String(base64EncodedText);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }
    }
}

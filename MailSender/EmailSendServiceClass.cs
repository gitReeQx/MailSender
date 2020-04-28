using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net;
using System.Net.Mail;

namespace MailSender
{
    public class EmailSendServiceClass
    {
        List<string> listStrMails;  // Список email'ов кому мы отправляем письмо
        string strPassword;  
        string mailBody;
        string mailTitle;

        public EmailSendServiceClass(List<string> _listStrMails, string _strPassword, string _mailBody, string _mailTitle)
        {
            listStrMails = _listStrMails;
            strPassword = _strPassword;
            mailBody = _mailBody;
            mailTitle = _mailTitle;
        }

        public void SendMail()
        {
            
            foreach (string mail in listStrMails)
            {
                // Используем using, чтобы гарантированно удалить объект MailMessage после использования
                using (MailMessage mm = new MailMessage("Reeqx@yandex.ru", mail))
                {
                    // Формируем письмо
                    mm.Subject = mailTitle;// Заголовок письма
                    mm.Body = mailBody;       // Тело письма
                    mm.IsBodyHtml = false;           // Не используем html в теле письма
                                                     // Авторизуемся на smtp-сервере и отправляем письмо
                                                     // Оператор using гарантирует вызов метода Dispose, даже если при вызове 
                                                     // методов в объекте происходит исключение.
                    using (SmtpClient sc = new SmtpClient(StaticMembers.SMTPServer, StaticMembers.Port))
                    {
                        sc.EnableSsl = true;
                        sc.Credentials = new NetworkCredential("ReeQx", strPassword);
                        try
                        {
                            sc.Send(mm);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Невозможно отправить письмо " + ex.ToString());
                        }
                    }
                } 
            }

        }
    }
}

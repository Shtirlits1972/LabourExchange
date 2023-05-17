using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Configuration;
using System.Net.Mail;
using System.Net;
using System.Xml;
using System.Windows;
using CommunityToolkit.Mvvm.DependencyInjection;
using LabourExchange.Model;
using CommunityToolkit.Mvvm.Messaging;
using System.Runtime.Remoting.Messaging;
using static OfficeOpenXml.ExcelErrorValue;
using System.Windows.Input;

namespace LabourExchange
{
    public class Ut
    {
        public static Users currentUser = new Users();

        public static readonly string strConn = ConfigurationManager.AppSettings["SqlConnString"];

        public static string[] SexArr = {"Муж", "Жен" };

        public static bool IsMoreThen16(DateTime dateTime)
        {
            bool flag = false;
            double dub16Years = 16 * 365; // 5840
            TimeSpan timeSpan = DateTime.Now - dateTime;

            if(timeSpan.TotalDays >= dub16Years)
            {
                flag = true;
            }

            return flag;
        }

        public static void SendEmailWithAttachment(string recipient, string subject, string body, string attachmentPath)
        {
            try
            {
                string login = ConfigurationManager.AppSettings["login"];   // doc.SelectSingleNode("//appSettings/add[@key='login']").Attributes["value"].Value;
                string password = ConfigurationManager.AppSettings["password"];   //doc.SelectSingleNode("//appSettings/add[@key='password']").Attributes["value"].Value;
                string smtpServer = ConfigurationManager.AppSettings["smtp"];
                int smtpPort = int.Parse(ConfigurationManager.AppSettings["port"]);
                bool ssl = bool.Parse(ConfigurationManager.AppSettings["ssl"]);

                // Создаем клиент SMTP
                SmtpClient smtpClient = new SmtpClient(smtpServer, smtpPort);
                smtpClient.Credentials = new NetworkCredential(login, password);
                smtpClient.EnableSsl = ssl;

                // Создаем объект MailMessage
                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress(login);
                mailMessage.To.Add(recipient);
                mailMessage.Subject = subject;
                mailMessage.Body = body;
                Attachment attachment = new Attachment(attachmentPath);
                mailMessage.Attachments.Add(attachment);

                // Отправляем письмо
                smtpClient.Send(mailMessage);
                MessageBox.Show("Почта отправлена","Внимание!", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}

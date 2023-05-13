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

namespace LabourExchange
{
    public class Ut
    {
        public static Users currentUser = new Users();

        public static readonly string strConn = ConfigurationManager.AppSettings["SqlConnString"];

        public static string[] SexArr = {"Муж", "Жен" };

        public static void SendEmailWithAttachment(string recipient, string subject, string body, string attachmentPath)
        {
            try
            {
                var Login = ConfigurationManager.AppSettings["login"];   // doc.SelectSingleNode("//appSettings/add[@key='login']").Attributes["value"].Value;
                var Password = ConfigurationManager.AppSettings["password"];   //doc.SelectSingleNode("//appSettings/add[@key='password']").Attributes["value"].Value;

                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                string smtpServer = "smtp.mail.ru";
                int smtpPort = 587;
                string login = Login;
                string password = Password;

                // Создаем клиент SMTP
                SmtpClient smtpClient = new SmtpClient(smtpServer, smtpPort);
                smtpClient.Credentials = new NetworkCredential(login, password);
                smtpClient.EnableSsl = true;

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
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}

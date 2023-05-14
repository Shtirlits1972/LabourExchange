using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
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
using System.Windows.Shapes;
using System.Xml;
using static OfficeOpenXml.ExcelErrorValue;

namespace LabourExchange.Forms
{
    public partial class SendMailParams : Window
    {
        public SendMailParams()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            txtLogin.Text = ConfigurationManager.AppSettings["login"];
            txtPassword.Text = ConfigurationManager.AppSettings["password"];

            txtSmtp.Text = ConfigurationManager.AppSettings["smtp"];
            txtPort.Text = ConfigurationManager.AppSettings["port"];
            checkSSL.IsChecked = bool.Parse(ConfigurationManager.AppSettings["ssl"]);
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                configuration.AppSettings.Settings["login"].Value = txtLogin.Text;
                configuration.AppSettings.Settings["password"].Value = txtPassword.Text;

                configuration.AppSettings.Settings["smtp"].Value = txtSmtp.Text;
                configuration.AppSettings.Settings["port"].Value = txtPort.Text;
                configuration.AppSettings.Settings["ssl"].Value = checkSSL.IsChecked.Value.ToString();

                configuration.Save(ConfigurationSaveMode.Full, true);
                ConfigurationManager.RefreshSection("appSettings");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            Close();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}

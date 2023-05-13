using LabourExchange.CRUD;
using LabourExchange.Model;
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
using System.Windows.Shapes;

namespace LabourExchange.Forms
{
    public partial class Login : Window
    {
        public bool IsRegister = false;
        public Login()
        {
            InitializeComponent();
        }

        private void btnEnter_Click(object sender, RoutedEventArgs e)
        {
            string strLogin = txtLogin.Text;
            string strPassword = txtPassword.Text;

          Ut.currentUser = UsersCrud.Login(strLogin, strPassword);

            if(Ut.currentUser == null)
            {
                MessageBox.Show("Неправильный логин и(или) пароль!", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Stop); 
                return;
            }
            else
            {
                Close();
            }
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            IsRegister = true;
            Close();
        }
    }
}

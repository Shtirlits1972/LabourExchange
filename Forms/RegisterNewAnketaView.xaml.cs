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

    public partial class RegisterNewAnketaView : Window
    {
        public bool IsReg = false;
        public RegisterNewAnketaView()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            bool LoginIsFree = UsersCrud.checkLogin(txtLogin.Text.Trim());

            if(!LoginIsFree)
            {
                MessageBox.Show("Выберите другой логин для регистрации!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            Users users = new Users { Id = 0,  Login = txtLogin.Text, Password = txtPassword.Text, Role = "user" };
            Ut.currentUser = UsersCrud.Add(users);

            IsReg = true;

            Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}

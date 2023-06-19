using LabourExchange.CRUD;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace LabourExchange.Forms
{
    public partial class LoginPage : System.Windows.Controls.Page
    {
        public bool IsRegister = false;
        public LoginPage()
        {
            InitializeComponent();
        }

        private void btnEnter_Click(object sender, RoutedEventArgs e)
        {
            string strLogin = txtLogin.Text;
            string strPassword = txtPassword.Text;

            Ut.currentUser = UsersCrud.Login(strLogin, strPassword);

            if (Ut.currentUser == null)
            {
                MessageBox.Show("Неправильный логин и(или) пароль!", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Stop);
                return;
            }
            else
            {
                MainWindow window = (MainWindow)Application.Current.MainWindow;
                window.MenuExit.Visibility = Visibility.Visible;
                window.frmContent.NavigationService.Navigate(new Uri("Forms/ScreenPage.xaml", UriKind.Relative));

                if (Ut.currentUser.Role == "admin")
                {
                    window.MenuReferences.Visibility = Visibility.Visible;
                    window.MenuBezWorkFindJob.Visibility = Visibility.Visible;
                    window.MenuFindInPage.Visibility = Visibility.Visible;
                    window.MenuEmployment.Visibility = Visibility.Visible;
                    window.MenuParam.Visibility = Visibility.Visible;
                }
                else
                {
                    window.frmContent.NavigationService.Navigate(new Uri("Forms/AnketaUserPage.xaml", UriKind.Relative));
                    Title = "Личный кабинет";
                }
            }
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            RegisterNewAnketaView registerNew = new RegisterNewAnketaView();
            registerNew.ShowDialog();

            if (registerNew.IsReg)
            {
                MainWindow window = (MainWindow)Application.Current.MainWindow;
                window.MenuExit.Visibility = Visibility.Visible;
                window.frmContent.NavigationService.Navigate(new Uri("Forms/AnketaUserPage.xaml", UriKind.Relative));
                Title = "Личный кабинет";
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.Close();
        }
    }
}

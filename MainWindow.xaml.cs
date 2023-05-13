using LabourExchange.Forms;
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

namespace LabourExchange
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Login login = new Login();
            login.ShowDialog();

            if(login.IsRegister)
            {
                RegisterNewAnketaView registerNew = new RegisterNewAnketaView();
                registerNew.ShowDialog();

                if(!registerNew.IsReg)
                {
                    Close();
                }
            }

            if(Ut.currentUser.Role == "user")
            {
                MenuReferences.Visibility = Visibility.Collapsed;
            }
        }

        private void ExitClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Education_Click(object sender, RoutedEventArgs e)
        {
            frmContent.NavigationService.Navigate(new Uri("Forms/EducationPage.xaml", UriKind.Relative));
            Title = "Образование";
        }

        private void Firma_Click(object sender, RoutedEventArgs e)
        {
            frmContent.NavigationService.Navigate(new Uri("Forms/FirmaPage.xaml", UriKind.Relative));
            Title = "Фирмы";
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
          if(MessageBox.Show("Хотите выйти?", "Внимание!", MessageBoxButton.OKCancel, MessageBoxImage.Warning) == MessageBoxResult.OK)
                {
                 Close();
            }
        }

        private void FamilyStatusBtn_Click(object sender, RoutedEventArgs e)
        {
            frmContent.NavigationService.Navigate(new Uri("Forms/FamilyStatusPage.xaml", UriKind.Relative));
            Title = "Семейное положение";
        }

        private void PositionPageBtn_Click(object sender, RoutedEventArgs e)
        {
            frmContent.NavigationService.Navigate(new Uri("Forms/PositionPage.xaml", UriKind.Relative));
            Title = "Должность";
        }

        private void AnketaPageBtn_Click(object sender, RoutedEventArgs e)
        {
            frmContent.NavigationService.Navigate(new Uri("Forms/AnketaPage.xaml", UriKind.Relative));
            Title = "Анкеты";
        }

        private void MenuSchedule_Click(object sender, RoutedEventArgs e)
        {
            frmContent.NavigationService.Navigate(new Uri("Forms/WorkScedulePage.xaml", UriKind.Relative));
            Title = "График работы";
        }

        private void Menubenefit_Click(object sender, RoutedEventArgs e)
        {
            frmContent.NavigationService.Navigate(new Uri("Forms/BenefitPage.xaml", UriKind.Relative));
            Title = "Пособия";
        }

        private void MenuVacancy_Click(object sender, RoutedEventArgs e)
        {
            frmContent.NavigationService.Navigate(new Uri("Forms/VacancyPage.xaml", UriKind.Relative));
            Title = "Вакансии";
        }

        private void MenuBezWork_Click(object sender, RoutedEventArgs e)
        {
            frmContent.NavigationService.Navigate(new Uri("Forms/BezWorkPage.xaml", UriKind.Relative));
            Title = "Безработные";
        }

        private void MenuFindVacansia_Click(object sender, RoutedEventArgs e)
        {
            frmContent.NavigationService.Navigate(new Uri("Forms/FindVacancyPage.xaml", UriKind.Relative));
            Title = "Поиск вакансий";
        }

        private void MenuFindBez_Work_Click(object sender, RoutedEventArgs e)
        {
            frmContent.NavigationService.Navigate(new Uri("Forms/FindBezWorkPage.xaml", UriKind.Relative));
            Title = "Поиск безработных";
        }

        private void MenuEmployment_Click(object sender, RoutedEventArgs e)
        {
            frmContent.NavigationService.Navigate(new Uri("Forms/EmploymentPage.xaml", UriKind.Relative));
            Title = "Трудоустройство";
        }

        private void MenuParam_Click(object sender, RoutedEventArgs e)
        {
            SendMailParams sendMailParams = new SendMailParams();
            sendMailParams.ShowDialog();
        }
    }
}

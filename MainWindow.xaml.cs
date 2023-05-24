using LabourExchange.Forms;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Telerik.Windows.Controls;

namespace LabourExchange
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            LocalizationManager.Manager = new CustomLocalizationManager();
            InitializeComponent();
            this.Closing += MainWindow_Closing;
        }

        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (MessageBox.Show("Хотите выйти?", "Внимание!", MessageBoxButton.OKCancel, MessageBoxImage.Warning) == MessageBoxResult.OK)
            {
                e.Cancel = false;
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //Login login = new Login();
            //login.Owner = this;
            //login.ShowDialog();

            //if (login.IsRegister)
            //{
            //    RegisterNewAnketaView registerNew = new RegisterNewAnketaView();
            //    registerNew.ShowDialog();

            //    if (!registerNew.IsReg)
            //    {
            //        Close();
            //    }
            //}

            Users users = new Users { Id = 1, Login = "admin", Role = "admin" };
            Ut.currentUser = users;

            MenuExit.Visibility = Visibility.Visible;

            if (Ut.currentUser.Role == "admin")
            {
                MenuReferences.Visibility = Visibility.Visible;
                MenuBezWorkFindJob.Visibility = Visibility.Visible;
                MenuFindInPage.Visibility = Visibility.Visible;
                MenuEmployment.Visibility = Visibility.Visible;
                MenuParam.Visibility = Visibility.Visible;
            }
            else
            {
                frmContent.NavigationService.Navigate(new Uri("Forms/AnketaUserPage.xaml", UriKind.Relative));
                Title = "Личный кабинет";
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
            this.Close();
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
            frmContent.NavigationService.Navigate(new Uri("Forms/AnketaPage2.xaml", UriKind.Relative));
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

        private void MenuFindInPage_Click(object sender, RoutedEventArgs e)
        {
            frmContent.NavigationService.Navigate(new Uri("Forms/Find_Vacancy_BezWork_Page.xaml", UriKind.Relative));
            Title = "Поиск";
        }


    }
}

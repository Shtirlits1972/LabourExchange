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

namespace LabourExchange.Forms
{

    public partial class Find_Vacancy_BezWork_Page : Page
    {
        public Find_Vacancy_BezWork_Page()
        {
            InitializeComponent();
            vacancyGrid.ItemsSource = VacancyCrud.GetAll();
            bezWorkGrid.ItemsSource = BezWorkCrud.GetAll();
        }

        private void btnClearFilterVacancy_Click(object sender, RoutedEventArgs e)
        {
            vacancyGrid.FilterDescriptors.SuspendNotifications();
            foreach (Telerik.Windows.Controls.GridViewColumn column in vacancyGrid.Columns)
            {
                column.ClearFilters();
            }
            vacancyGrid.FilterDescriptors.ResumeNotifications();
        }

        private void btnClearFilterBezWork_Click(object sender, RoutedEventArgs e)
        {
            bezWorkGrid.FilterDescriptors.SuspendNotifications();
            foreach (Telerik.Windows.Controls.GridViewColumn column in bezWorkGrid.Columns)
            {
                column.ClearFilters();
            }
            bezWorkGrid.FilterDescriptors.ResumeNotifications();
        }
    }
}
